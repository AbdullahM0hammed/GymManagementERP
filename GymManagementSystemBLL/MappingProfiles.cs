using AutoMapper;
using GymManagementSystemBLL.Services.ViewModeIs.MemberViewModeIs;
using GymManagementSystemBLL.Services.ViewModeIs.PlanViewModels;
using GymManagementSystemBLL.Services.ViewModeIs.SessionViewModeIs;
using GymManagementSystemBLL.Services.ViewModeIs.TrainerViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using GymManagementSystemDAL.Entity;

namespace GymManagementSystemBLL
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            MapSession();
            MapTrainer();
            MapMember();
            MapPlan();
        }
        private void MapSession()
        {
            CreateMap<Sessoin, SessionViewModel>()
                      .ForMember(dest => dest.CategoryName, Options => Options.MapFrom(src => src.Category.Name))
                      .ForMember(dest => dest.TrainerName, Options => Options.MapFrom(src => src.Trainer.Name))
                      .ForMember(dest => dest.AvailableSlots, options => options.Ignore());
            CreateMap<CreateSessionViewModel, Sessoin>();
            CreateMap<Sessoin, UpdateSessionViewModel>().ReverseMap();
            CreateMap<Trainer, TrainerSelectViewModel>();
            CreateMap<Category, CategorySelectViewModel>()
                .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name));
        }
        private void MapTrainer()
        {
            CreateMap<CreatetrainerViewmodel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    streeet = src.Street,
                    City = src.City
                }));
            CreateMap<Trainer, TrainerViewModel>();
            CreateMap<Trainer, UpdataedTrinerViewModel>()
                .ForMember(dist => dist.Street, opt => opt.MapFrom(src => src.Address.streeet))
                .ForMember(dist => dist.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dist => dist.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber));

            CreateMap<UpdataedTrinerViewModel, Trainer>()
            .ForMember(dest => dest.Name, opt => opt.Ignore())
            .AfterMap((src, dest) =>
            {
                dest.Address.BuildingNumber = src.BuildingNumber;
                dest.Address.City = src.City;
                dest.Address.streeet = src.Street;
                dest.UpdatedAT = DateTime.Now;
            });
        }
        private void MapMember()
        {
            CreateMap<CreateMemberViewModel, Member>()
                  .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                  {
                      BuildingNumber = src.BuildingNumber,
                      City = src.City,
                      streeet = src.Street
                  })).ForMember(dest => dest.HealthRecord, opt => opt.MapFrom(src => src.HealtRecordview));


            CreateMap<HealtRecordViewModel, HealthRecord>().ReverseMap();
            CreateMap<Member, MemberViewModel>()
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.streeet} - {src.Address.City}"));

            CreateMap<Member, MemberToUpdateViewModel>()
            .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
            .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.streeet));

            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.City = src.City;
                    dest.Address.streeet = src.Street;
                    dest.UpdatedAT = DateTime.Now;
                });
        }
        private void MapPlan()
        {
            CreateMap<Plan, PlanViewmodel>();
            CreateMap<Plan, UpdatePlanViewmodel>().ForMember(dest => dest.PlanName, opt => opt.MapFrom(src => src.Name));
            CreateMap<UpdatePlanViewmodel, Plan>()
           .ForMember(dest => dest.Name, opt => opt.Ignore())
           .ForMember(dest => dest.UpdatedAT, opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
