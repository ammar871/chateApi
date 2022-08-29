using System;
using System.Threading.Tasks;
using AutoMapper;
using chatApi.Data;
using chatApi.Models;
using chatApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chatApi.Helpers;
using System.Collections.Generic;
using System.Linq;

using System.Text.Json;


namespace chatApi.Controllers
{


    [Route("message")]
    [ApiController]
    public class MessagesController : ControllerBase
    {




        private readonly DBContext _context;
        private IMapper _mapper;
        public MessagesController(IMapper mapper, DBContext context)
        {

            _mapper = mapper;
            _context = context;

        }



        [HttpGet]
        [Route("get-Messages")]
        public async Task<ActionResult> GetAll([FromQuery]PagingParameterModel  @params)
        {
            var data = await _context.Messages.ToListAsync();
            

            var paginationMetadata = new PaginationMetadata(data.Count(), @params.Page, @params.ItemsPerPage);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            var items =  data.Skip((@params.Page - 1) * @params.ItemsPerPage)
                                       .Take(@params.ItemsPerPage);
         return Ok(new {

             items=items,
             currentPage=@params.Page,
              totalPage=paginationMetadata.TotalPages
    });

        }



         [HttpGet]
        [Route("get-MessagesById")]
        public async Task<ActionResult> GetAllById([FromForm]PagingParameterModel  @params,[FromForm] string userId,[FromForm] string senderId)
        {
            var data = await _context.Messages.OrderByDescending(x => x.CreatedAt).Where(x=>(userId == x.UserId && senderId==x.SenderId)||
            (userId == x.SenderId && senderId==x.UserId)).ToListAsync();
            
            foreach(var item in data){
              if(item.UserId==userId){
               item.TypeSender="user";

              }else{
                item.TypeSender="sender";
              }
             await _context.SaveChangesAsync();
            }

            var paginationMetadata = new PaginationMetadata(data.Count(), @params.Page, @params.ItemsPerPage);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            var items =  data.Skip((@params.Page - 1) * @params.ItemsPerPage)
                                       .Take(@params.ItemsPerPage);
         return Ok(new {

             items=items,
             currentPage=@params.Page,
              totalPage=paginationMetadata.TotalPages
    });

        }




        [HttpPost]
        [Route("add-message")]
        public async Task<ActionResult> CreateMessage([FromForm] CreateMessageDto messageDto)
        {

            // create room first 
            // check idRoom 
            // updateRoom 
            // create message

                Room newRoom = new Room{
                    Message = messageDto.MessageText,
                    SenderId=messageDto.SenderId,
                     UserId=messageDto.UserId,
                    Type= messageDto.Type,
                    Status=messageDto.Status
                  };



            Room checkRoom=await _context.Rooms.FirstOrDefaultAsync(x=>(messageDto.UserId == x.UserId && messageDto.SenderId==x.SenderId)||
            (messageDto.UserId == x.SenderId && messageDto.SenderId==x.UserId) );


            if(checkRoom==null){

              await _context.Rooms.AddAsync(newRoom);
              
              await _context.SaveChangesAsync();
              messageDto.RoomId=newRoom.Id;
            }else {

                   CreateRoomDto createRoomDto = new CreateRoomDto{
                    Message = messageDto.MessageText,
                    SenderId=messageDto.SenderId,
                     UserId=messageDto.UserId,
                    Type= messageDto.Type,
                    Status=messageDto.Status
                  };

                 _mapper.Map(createRoomDto, checkRoom);

              await   _context.SaveChangesAsync();
               messageDto.RoomId=checkRoom.Id;
                    }
             

            var messageModel = _mapper.Map<Message>(messageDto);
            if (messageDto == null)
            {
                throw new ArgumentNullException(nameof(messageDto));
            }

            await _context.Messages.AddAsync(messageModel);

            // notification
        User user =await _context.Users.FirstOrDefaultAsync(x => x.Id == messageDto.SenderId);

        await Functions.slt.SendNotificationAsync( messageModel.SenderId,"رسالة جديدة", messageDto.MessageText,false,_context,messageModel.UserId);
            await _context.SaveChangesAsync();
            return Ok(messageModel);


        }



        [HttpPost]
        [Route("delete-message")]
        public async Task<ActionResult> DeleteMessage([FromForm] int id)
        {

            var messageModelFromRepo =await _context.Messages.FirstOrDefaultAsync(p => p.Id==id);
            if (messageModelFromRepo == null)
            {
                return NotFound();
            }

            _context.Remove(messageModelFromRepo);
                await _context.SaveChangesAsync();
            return Ok(messageModelFromRepo);



        }




        [HttpPost]
        [Route("update-message")]
        public async Task<ActionResult> UpdateMessage([FromForm] int id, [FromForm] CreateMessageDto message)
        {
            var messageModel =await _context.Messages.FirstOrDefaultAsync(p => p.Id==id);
            if (messageModel == null)
            {
                return NotFound();
            }


            _mapper.Map(message, messageModel);

          
          await  _context.SaveChangesAsync();

            return NoContent();

        }

    }



}