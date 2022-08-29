using System;
using System.Threading.Tasks;
using AutoMapper;
using chatApi.Data;
using chatApi.Models;
using chatApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using System.Text.Json;
namespace chatApi.Controllers
{


    [Route("Room")]
    [ApiController]
    public class RoomsController : ControllerBase
    {




        private readonly DBContext _context;
        private IMapper _mapper;
        public RoomsController(IMapper mapper, DBContext context)
        {

            _mapper = mapper;
            _context = context;

        }



        [HttpGet]
        [Route("get-rooms")]
        public async Task<ActionResult> GetAll([FromQuery]PagingParameterModel  @params)
        {
            var data = await _context.Rooms.ToListAsync();
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
        [Route("get-roomsById")]
        public async Task<ActionResult> GetAllById([FromForm]PagingParameterModel  @params,[FromForm] string userId)
        {


   List<ResponseRoom> roomDetails=new List<ResponseRoom>();

            var data = await _context.Rooms.Where(x => userId==x.UserId||userId == x.SenderId).ToListAsync();

         foreach(var item in data)
           {
               UserDetailResponse senderDetailResponse;

             if(item.UserId ==userId){

              User user=_context.Users.FirstOrDefault(x=> x.Id==item.SenderId);
             senderDetailResponse= _mapper.Map<UserDetailResponse>(user);
             }else
             {
                 User user=_context.Users.FirstOrDefault(x=> x.Id==item.UserId);
               senderDetailResponse= _mapper.Map<UserDetailResponse>(user);
             }
            




             roomDetails.Add(new ResponseRoom{
                    room=item,
                  
                   Sender=senderDetailResponse
             }
            
             );


           }


          var paginationMetadata = new PaginationMetadata(roomDetails.Count(), @params.Page, @params.ItemsPerPage);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            var items =  roomDetails.Skip((@params.Page - 1) * @params.ItemsPerPage)
                                       .Take(@params.ItemsPerPage);


        //  User User = data.FirstOrDefault(x => x.UserId != userId);
                                   
         return Ok(new {
          
             items=roomDetails,
             currentPage=@params.Page,
              totalPage=paginationMetadata.TotalPages
          });
        }





        [HttpPost]
        [Route("add-room")]
        public async Task<ActionResult> CreateRoom([FromForm] CreateRoomDto roomDto)
        {

            var roomModel = _mapper.Map<Room>(roomDto);
            if (roomDto == null)
            {
                throw new ArgumentNullException(nameof(roomDto));
            }

            await _context.Rooms.AddAsync(roomModel);
            // var commandReadDto = _mapper.Map<roomReadDto>(roomModel);
            await _context.SaveChangesAsync();

            return Ok(roomModel);


        }



        [HttpPost]
        [Route("delete-room")]
        public async Task<ActionResult> DeleteRoom([FromForm] int id)
        {

            var roomModelFromRepo =await _context.Rooms.FirstOrDefaultAsync(p => p.Id==id);
            if (roomModelFromRepo == null)
            {
                return NotFound();
            }

            _context.Remove(roomModelFromRepo);
                await _context.SaveChangesAsync();
            return Ok(roomModelFromRepo);



        }




        [HttpPost]
        [Route("update-room")]
        public async Task<ActionResult> UpdateRoom([FromForm] int id, [FromForm] CreateRoomDto room)
        {
            var brandModelFromRepo =await _context.Rooms.FirstOrDefaultAsync(p => p.Id==id);
            if (brandModelFromRepo == null)
            {
                return NotFound();
            }


            _mapper.Map(room, brandModelFromRepo);

          
          await  _context.SaveChangesAsync();

            return NoContent();

        }

    }



}