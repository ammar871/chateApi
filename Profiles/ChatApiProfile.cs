using AutoMapper;
using chatApi.Models;
using chatApi.Dtos;
namespace chatApi.Profiles
{
    public class ChatApiProfile : Profile
    {
        


   public ChatApiProfile()
        {
           
            //User
            CreateMap<UserForRegister, User>();


            CreateMap<User, UserDetailResponse>();
            CreateMap<UserForUpdate, User>();
          

        //     CreateMap<DriverForRegister, Driver>(); 


          
            CreateMap<CreateMessageDto, Message>();
            CreateMap<CreateRoomDto, Room>();
            CreateMap<CreateGroupDto, Group>();
            // CreateMap<CreateCategory, Category>();
            // CreateMap<SubCategoryCreateDto, Category>();
     
            // CreateMap<SubCategoryUpdateDto, SubCategory>();

            //  CreateMap<CreateAdds, Adds>();
            // CreateMap<ReadAddsDto, Adds>();
            //  CreateMap<AddsUpdateDto, Adds>();





        }

        
    }
}
