using DAL.Repository;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.DTO;
using Services.Response;

namespace Services.Services.PayrollService
{
    public class PayrollService : IPayrollService
    {
        private readonly IRepository<Payroll> _payrollRepository;
        private readonly IRepository<PayrollTemp> _payrollTempRepository;
        private readonly IRepository<PayrollSetting> _payrollSettingRepository;

        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Worksite> _worksiteRepository;
        private readonly IRepository<WorksiteAction> _worksiteActionRepository;
        private readonly IRepository<WorksiteWorker> _worksiteWorkerRepository;



        private readonly IUnitOfWork _unitOfWork;


        public PayrollService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _payrollRepository = _unitOfWork.GetRepository<Payroll>();
            _payrollTempRepository = _unitOfWork.GetRepository<PayrollTemp>();
            _payrollSettingRepository = _unitOfWork.GetRepository<PayrollSetting>();

            _userRepository = _unitOfWork.GetRepository<User>();
            _worksiteRepository = _unitOfWork.GetRepository<Worksite>();
            _worksiteActionRepository = _unitOfWork.GetRepository<WorksiteAction>();
            _worksiteWorkerRepository = _unitOfWork.GetRepository<WorksiteWorker>();
 
        }

        public async Task<BaseResponse> GetAllPayrollSettingsAsync()
        {
            var entities = await _payrollSettingRepository.GetAllAsQueryable().Include(x => x.Month).Select(x => new PayrollSettingDTO()
            {
                Id = x.Id,
                YearId = x.YearId,
                MonthId = x.MonthId,
                MonthName = x.Month.Name,
                IsApproved = x.IsApproved
            }).ToListAsync();
            return new BaseResponse(true, "", entities);
        }
        public async Task<BaseResponse> GetPayrollSettingByIdAsync(int id)
        {
            var entity = await _payrollSettingRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return new BaseResponse(false, "PayrollSetting Could Not Found", null);
            }

            var entityDTO = new PayrollSettingDTO()
            {
                Id = entity.Id,
                YearId = entity.YearId,
                MonthId = entity.MonthId,
                IsApproved = entity.IsApproved
            };
            return new BaseResponse(true, "", entityDTO);

        }
        public async Task<BaseResponse> CreatePayrollSettingAsync(PayrollSettingDTO request)
        {
            var entity = new PayrollSetting()
            {
                YearId = request.YearId,
                MonthId = request.MonthId,
                IsApproved = request.IsApproved
            };
            await _payrollSettingRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new PayrollSettingDTO()
            {
                Id = entity.Id,
                YearId = entity.YearId,
                MonthId = entity.MonthId,
                IsApproved = entity.IsApproved
            });
        }
        public async Task<BaseResponse> UpdatePayrollSettingAsync(PayrollSettingDTO request)
        {
            var entity = await _payrollSettingRepository.GetByIdAsync(request.Id);
            if (entity is null)
            {
                return new BaseResponse(false, "PayrollSetting not found!", null);

            }
            entity.YearId = request.YearId;
            entity.MonthId = request.MonthId;
            entity.IsApproved = request.IsApproved;

            await _payrollSettingRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new PayrollSettingDTO()
            {
                Id = entity.Id,
                YearId = entity.YearId,
                MonthId = entity.MonthId,
                IsApproved = entity.IsApproved
            });
        }
        public async Task<BaseResponse> DeletePayrollSettingAsync(int id)
        {
            var entity = await _payrollSettingRepository.GetByIdAsync(id);
            if (entity is not null)
            {
                await _payrollSettingRepository.DeleteAsync(entity);
                await _unitOfWork.CommitAsync();
                return new BaseResponse(true, "", null);
            }
            return new BaseResponse(false, "PayrollSetting Type Could Not Found", null);
        }

        public async Task<BaseResponse> GetAllPayrollsAsync(PayrollFilterDTO request)
        {
            var entities = await _payrollRepository
                .GetAllAsQueryable()
                .Where(x=>(request.YearId==null || x.YearId==request.YearId) && (request.MonthId == null || x.MonthId == request.MonthId) && (request.UserId == null || x.UserId == request.UserId))
                .Include(x=>x.User).Include(x=>x.Month)
                .Select(x=>new PayrollDTO() {
            Id = x.Id,
            UserId  = x.UserId,
            UserName = x.User.Name + " " + x.User.SurName,
            YearId = x.YearId,
            MonthId = x.MonthId,
            MonthName = x.Month.Name,
            Salary = x.Salary,
            Overtime = x.Overtime
            }).ToListAsync();
            return new BaseResponse(true, "", entities);
        }
        public async Task<BaseResponse> GetPayrollByIdAsync(int id)
        {
            var entity =  await _payrollRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return new BaseResponse(false,"Payroll Could Not Found",null);
            }

            var entityDTO = new PayrollDTO()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                YearId = entity.YearId,
                MonthId = entity.MonthId,
                Salary = entity.Salary,
                Overtime = entity.Overtime
            };
            return new BaseResponse(true, "", entityDTO);

        }
        public async Task<BaseResponse> CreatePayrollAsync(PayrollDTO request)
        {
            var entity  = new Payroll() 
            { UserId = request.UserId, 
                YearId = request.YearId, 
                MonthId = request.MonthId, 
                Salary = request.Salary,
                Overtime = request.Overtime
            };
            await _payrollRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new PayrollDTO()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                YearId = entity.YearId,
                MonthId = entity.MonthId,
                Salary = entity.Salary,
                Overtime = entity.Overtime
            });
        }
        public async Task<BaseResponse> UpdatePayrollAsync(PayrollDTO request)
        {
            var entity = await _payrollRepository.GetByIdAsync(request.Id);
            if (entity is null)
            {
                return new BaseResponse(false, "Payroll not found!", null);

            }
            entity.UserId = request.UserId;
            entity.YearId = request.YearId;
            entity.MonthId = request.MonthId;
            entity.Salary = request.Salary;
            entity.Overtime = request.Overtime;

            await _payrollRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new PayrollDTO()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                YearId = entity.YearId,
                MonthId = entity.MonthId,
                Salary = entity.Salary,
                Overtime = entity.Overtime
            });
        }
        public async Task<BaseResponse> DeletePayrollAsync(int id)
        {
            var entity = await _payrollRepository.GetByIdAsync(id);
            if (entity is not null)
            {
                await _payrollRepository.DeleteAsync(entity);
                await _unitOfWork.CommitAsync();
                return new BaseResponse(true,"",null);
            }
            return new BaseResponse(false, "Payroll Type Could Not Found", null);
        }

        public async Task<BaseResponse> GetAllPayrollTempsAsync(PayrollTempFilterDTO request)
        {
            var entities = await _payrollTempRepository.GetAllAsQueryable()
                .Where(x => (request.YearId == null || x.YearId == request.YearId) && (request.MonthId == null || x.MonthId == request.MonthId) && (request.UserId == null || x.UserId == request.UserId))
                .Include(x => x.User).Include(x => x.Month)
                .Select(x => new PayrollTempDTO()
            {
                Id = x.Id,
                UserId = x.UserId,
                UserName = x.User.Name + " " + x.User.SurName,
                YearId = x.YearId,
                MonthId = x.MonthId,
                MonthName = x.Month.Name,
                Salary = x.Salary,
                Overtime = x.Overtime
            }).ToListAsync();
            return new BaseResponse(true, "", entities);
        }
        public async Task<BaseResponse> GetPayrollTempByIdAsync(int id)
        {
            var entity = await _payrollTempRepository.GetByIdAsync(id);
            if (entity is null)
            {
                return new BaseResponse(false, "PayrollTemp Could Not Found", null);
            }

            var entityDTO = new PayrollTempDTO()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                YearId = entity.YearId,
                MonthId = entity.MonthId,
                Salary = entity.Salary,
                Overtime = entity.Overtime
            };
            return new BaseResponse(true, "", entityDTO);

        }
        public async Task<BaseResponse> CreatePayrollTempAsync(PayrollTempDTO request)
        {
            var entity = new PayrollTemp()
            {
                UserId = request.UserId,
                YearId = request.YearId,
                MonthId = request.MonthId,
                Salary = request.Salary,
                Overtime = request.Overtime
            };
            await _payrollTempRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new PayrollTempDTO()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                YearId = entity.YearId,
                MonthId = entity.MonthId,
                Salary = entity.Salary,
                Overtime = entity.Overtime
            });
        }
        public async Task<BaseResponse> UpdatePayrollTempAsync(PayrollTempDTO request)
        {
            var entity = await _payrollTempRepository.GetByIdAsync(request.Id);
            if (entity is null)
            {
                return new BaseResponse(false, "PayrollTemp not found!", null);

            }
            entity.UserId = request.UserId;
            entity.YearId = request.YearId;
            entity.MonthId = request.MonthId;
            entity.Salary = request.Salary;
            entity.Overtime = request.Overtime;

            await _payrollTempRepository.UpdateAsync(entity);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", new PayrollTempDTO()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                YearId = entity.YearId,
                MonthId = entity.MonthId,
                Salary = entity.Salary,
                Overtime = entity.Overtime
            });
        }
        public async Task<BaseResponse> DeletePayrollTempAsync(int id)
        {
            var entity = await _payrollTempRepository.GetByIdAsync(id);
            if (entity is not null)
            {
                await _payrollTempRepository.DeleteAsync(entity);
                await _unitOfWork.CommitAsync();
                return new BaseResponse(true, "", null);
            }
            return new BaseResponse(false, "PayrollTemp Type Could Not Found", null);
        }

        public async Task<BaseResponse> Transfer(PayrollTransferDTO request)
        {
            var settingEntity = await _payrollSettingRepository.GetAllAsQueryable().Where(x=>x.YearId==request.YearId && x.MonthId==request.MonthId).FirstOrDefaultAsync();
            if (settingEntity is null)
            {
                return new BaseResponse(false, "There is no Payroll Settings in the system. To transfer PayrollTemps, you have to list them at least one time on Payroll Temps Page!", null);
            }
            else if (settingEntity.IsApproved)
            {
                return new BaseResponse(false, $"{request.MonthId}.{request.YearId} already been transferred before!", null);
            }

            var payrollEntities = await _payrollTempRepository
                                .GetAllAsQueryable()
                                .Where(x => x.YearId == request.YearId && x.MonthId == request.MonthId)
                                .Select(x=>new Payroll() { 
                                    YearId = x.YearId,
                                    MonthId = x.MonthId,
                                    UserId = x.UserId,
                                    Salary = x.Salary,
                                    Overtime = x.Overtime
                                })
                                .ToListAsync();
            if (payrollEntities.IsNullOrEmpty())
            {
                return new BaseResponse(false, $"There is no {request.MonthId}.{request.YearId} Payroll Temps Records in the system.", null);
            }

            await _payrollRepository.AddRangeAsync(payrollEntities);
            settingEntity.IsApproved = true;
            await _payrollSettingRepository.UpdateAsync(settingEntity);
            await _unitOfWork.CommitAsync();
            return new BaseResponse(true, "", null);
        }
        public async Task<BaseResponse> Refresh(PayrollRefreshDTO request)
        {
            if (request.YearId == 0)
            {
                return new BaseResponse(false, $"Year parameter could not be 0", null);
            }
            else if (request.MonthId == 0)
            {
                return new BaseResponse(false, $"MonthId parameter could not be 0", null);
            }

            var settingEntity = await _payrollSettingRepository.GetAllAsQueryable().Where(x => x.YearId == request.YearId && x.MonthId == request.MonthId).FirstOrDefaultAsync();
            if (settingEntity is null)
            {
                settingEntity = new PayrollSetting()
                {
                    YearId = request.YearId,
                    MonthId = request.MonthId,
                    IsApproved = false
                };
                await _payrollSettingRepository.AddAsync(settingEntity);
            }
            else if (settingEntity.IsApproved)
            {
                return new BaseResponse(false, $"{request.MonthId}.{request.YearId} already been transferred before! You can not Refresh it again", null);
            }

            var payrollTempEntities = await _payrollTempRepository
                    .GetAllAsQueryable()
                    .Where(x => x.YearId == request.YearId && x.MonthId == request.MonthId)
                    .ToListAsync();
            if (!payrollTempEntities.IsNullOrEmpty())
            {
                foreach (var payrollTempEntity in payrollTempEntities)
                {
                    await _payrollTempRepository.DeleteAsync(payrollTempEntity);
                }
            }

            DateTime firstDayOfMonth = new DateTime(request.YearId, request.MonthId, 1);
            int daysInMonth = DateTime.DaysInMonth(request.YearId, request.MonthId);
            DateTime lastDayOfMonth = new DateTime(request.YearId, request.MonthId, daysInMonth);



            var users = await _userRepository.GetAllAsQueryable().ToListAsync();

            var workerTimes = await _worksiteWorkerRepository.GetAllAsQueryable()
                            .Include(x=>x.WorksiteWorkerType)
                            .Where(x => (x.StartDate <= lastDayOfMonth && x.EndDate >= firstDayOfMonth))
                            .ToListAsync();

            workerTimes.ForEach(workerTime =>
            {
                if (workerTime.StartDate< firstDayOfMonth)
                {
                    workerTime.StartDate = firstDayOfMonth;
                }

                if (workerTime.EndDate > lastDayOfMonth)
                {
                    workerTime.EndDate = lastDayOfMonth;
                }
            });



            var actions = await _worksiteActionRepository.GetAllAsQueryable()
                            .Include(x => x.WorksiteActionType)
                            .Where(x => x.WorksiteActionType.Name == "Excavation Process" &&
                               x.Date.Year == request.YearId &&
                               x.Date.Month == request.MonthId
                            )
                            .ToListAsync();


            var results = workerTimes.GroupJoin(
                actions,
                workerTime => workerTime.WorksiteId, // Key from workerTimes
                action => action.WorksiteId, // Key from actions
                (workerTime, actionGroup) => new {
                    WorkerTime = workerTime,
                    // Filter the actions here to only include those within the start and end dates of the worker time
                    Actions = actionGroup
                                .Where(action => action.Date >= workerTime.StartDate && action.Date <= workerTime.EndDate)
                                //.DefaultIfEmpty() // Ensure it's a left join, includes null if no actions match
                                .ToList()
                })
               .ToList();



            var finalResults = users.GroupJoin(
                results,
                user => user.Id, // Assuming user.UserId is the key in the User entity
                result => result.WorkerTime.UserId, // Assuming WorkerTime.UserId is the key in the result from the previous join
                (user, resultsGroup) => new { User = user, 
                                                //Results = resultsGroup.DefaultIfEmpty() } // Results grouped by user
                                                Results = resultsGroup.ToList() } // Results grouped by user

            )
            .ToList();

            var entities = finalResults.Select(x => new PayrollTemp()
            {
                UserId = x.User.Id,
                YearId = request.YearId,
                MonthId = request.MonthId,
                Salary = x.User.Salary,
                Overtime = x.Results.Sum(y=> y.WorkerTime.WorksiteWorkerType.OvertimeWage * (y.Actions.Sum(z=> Convert.ToInt32(string.IsNullOrEmpty(z.Value) ? 0: z.Value))-700))
            }).ToList();
            entities.ForEach(entity =>
            {
                if (entity.Overtime < 0)
                {
                    entity.Overtime = 0;
                } 
            });

            await _payrollTempRepository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();

            return new BaseResponse(true, "", null);
        }
    }
}
