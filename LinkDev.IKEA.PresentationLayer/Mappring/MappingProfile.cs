using AutoMapper;
using LinkDev.IKEA.BusinesLogicLayer.Models.Departments;
using LinkDev.IKEA.PresentationLayer.ViewModels.Departments;

namespace LinkDev.IKEA.PresentationLayer.Mappring
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ctrl + k + s (to add region)
            // ctrl + D (to copy and past code line)
            // ctrl + k + D (to format the document)
            #region Employee

            #endregion

            #region Department

            CreateMap<DepartmentDetailsDto, DepartmentViewModel>();
            CreateMap<DepartmentViewModel, UpdatedDepartmentDto>();
            CreateMap<DepartmentViewModel, CreatedDepartmentDto>();
            #endregion
        }
    }
}
