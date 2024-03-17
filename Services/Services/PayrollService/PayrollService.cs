using DAL.Repository;
using DAL.UnitOfWork;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Services.DTO;
using Services.Response;

namespace Services.Services.PayrollService
{
    public class PayrollService : IPayrollService
    {
        private readonly IRepository<Payroll> _PayrollRepository;
        private readonly IRepository<PayrollTemp> _PayrollTempRepository;
        private readonly IRepository<PayrollSetting> _PayrollSettingRepository;

        private readonly IUnitOfWork _unitOfWork;


        public PayrollService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _PayrollRepository = _unitOfWork.GetRepository<Payroll>();
            _PayrollTempRepository = _unitOfWork.GetRepository<PayrollTemp>();
            _PayrollSettingRepository = _unitOfWork.GetRepository<PayrollSetting>();
        }

        public async Task<BaseResponse> GetAllPayrollSettingsAsync()
        {
            var entities = await _PayrollSettingRepository.GetAllAsQueryable().Include(x => x.Month).Select(x => new PayrollSettingDTO()
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
            var entity = await _PayrollSettingRepository.GetByIdAsync(id);
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
            await _PayrollSettingRepository.AddAsync(entity);
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
            var entity = await _PayrollSettingRepository.GetByIdAsync(request.Id);
            if (entity is null)
            {
                return new BaseResponse(false, "PayrollSetting not found!", null);

            }
            entity.YearId = request.YearId;
            entity.MonthId = request.MonthId;
            entity.IsApproved = request.IsApproved;

            await _PayrollSettingRepository.UpdateAsync(entity);
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
            var entity = await _PayrollSettingRepository.GetByIdAsync(id);
            if (entity is not null)
            {
                await _PayrollSettingRepository.DeleteAsync(entity);
                await _unitOfWork.CommitAsync();
                return new BaseResponse(true, "", null);
            }
            return new BaseResponse(false, "PayrollSetting Type Could Not Found", null);
        }

        public async Task<BaseResponse> GetAllPayrollsAsync()
        {
            var entities = await _PayrollRepository.GetAllAsQueryable().Include(x=>x.User).Include(x=>x.Month).Select(x=>new PayrollDTO() {
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
            var entity =  await _PayrollRepository.GetByIdAsync(id);
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
            await _PayrollRepository.AddAsync(entity);
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
            var entity = await _PayrollRepository.GetByIdAsync(request.Id);
            if (entity is null)
            {
                return new BaseResponse(false, "Payroll not found!", null);

            }
            entity.UserId = request.UserId;
            entity.YearId = request.YearId;
            entity.MonthId = request.MonthId;
            entity.Salary = request.Salary;
            entity.Overtime = request.Overtime;

            await _PayrollRepository.UpdateAsync(entity);
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
            var entity = await _PayrollRepository.GetByIdAsync(id);
            if (entity is not null)
            {
                await _PayrollRepository.DeleteAsync(entity);
                await _unitOfWork.CommitAsync();
                return new BaseResponse(true,"",null);
            }
            return new BaseResponse(false, "Payroll Type Could Not Found", null);
        }

        public async Task<BaseResponse> GetAllPayrollTempsAsync()
        {
            var entities = await _PayrollTempRepository.GetAllAsQueryable().Include(x => x.User).Include(x => x.Month).Select(x => new PayrollTempDTO()
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
            var entity = await _PayrollTempRepository.GetByIdAsync(id);
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
            await _PayrollTempRepository.AddAsync(entity);
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
            var entity = await _PayrollTempRepository.GetByIdAsync(request.Id);
            if (entity is null)
            {
                return new BaseResponse(false, "PayrollTemp not found!", null);

            }
            entity.UserId = request.UserId;
            entity.YearId = request.YearId;
            entity.MonthId = request.MonthId;
            entity.Salary = request.Salary;
            entity.Overtime = request.Overtime;

            await _PayrollTempRepository.UpdateAsync(entity);
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
            var entity = await _PayrollTempRepository.GetByIdAsync(id);
            if (entity is not null)
            {
                await _PayrollTempRepository.DeleteAsync(entity);
                await _unitOfWork.CommitAsync();
                return new BaseResponse(true, "", null);
            }
            return new BaseResponse(false, "PayrollTemp Type Could Not Found", null);
        }
    }
}
