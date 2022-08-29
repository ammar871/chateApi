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


    [Route("Group")]
    [ApiController]
    public class GroupsController : ControllerBase
    {




        private readonly DBContext _context;
        private IMapper _mapper;
        public GroupsController(IMapper mapper, DBContext context)
        {

            _mapper = mapper;
            _context = context;

        }



        [HttpGet]
        [Route("get-Groups")]
        public async Task<ActionResult> GetAll([FromQuery]PagingParameterModel  @params)
        {
            var data = await _context.Groups.ToListAsync();
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
        [Route("get-GroupsById")]
        public async Task<ActionResult> GetAllById([FromForm]PagingParameterModel  @params,[FromForm] string userId)
        {
            var data = await _context.Groups.Where(x =>x.Users.Contains(userId)||x.Admins.Contains(userId)).ToListAsync();
       
       
       
       
       
       
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
        [Route("add-group")]
        public async Task<ActionResult> CreateGroup([FromForm] CreateGroupDto groupDto)
        {

            var groupModel = _mapper.Map<Group>(groupDto);
            if (groupDto == null)
            {
                throw new ArgumentNullException(nameof(groupDto));
            }

            await _context.Groups.AddAsync(groupModel);
            // var commandReadDto = _mapper.Map<groupReadDto>(groupModel);
            await _context.SaveChangesAsync();

            return Ok(groupModel);


        }



        [HttpPost]
        [Route("delete-group")]
        public async Task<ActionResult> Deletegroup([FromForm] int id)
        {

            var groupModelFromRepo =await _context.Groups.FirstOrDefaultAsync(p => p.Id==id);
            if (groupModelFromRepo == null)
            {
                return NotFound();
            }

            _context.Remove(groupModelFromRepo);
                await _context.SaveChangesAsync();
            return Ok(groupModelFromRepo);



        }




        [HttpPost]
        [Route("update-group")]
        public async Task<ActionResult> Updategroup([FromForm] int id, [FromForm] CreateGroupDto group)
        {
            var brandModelFromRepo =await _context.Groups.FirstOrDefaultAsync(p => p.Id==id);
            if (brandModelFromRepo == null)
            {
                return NotFound();
            }


            _mapper.Map(group, brandModelFromRepo);

          
          await  _context.SaveChangesAsync();

            return NoContent();

        }




        [HttpPost]
        [Route("add-userToGroup")]
        public async Task<ActionResult> AddUserToGroup([FromForm] int id,[FromForm] string UserId, [FromForm] int Status )
        {
            var groupModelFromRepo =await _context.Groups.FirstOrDefaultAsync(p => p.Id==id);
            if (groupModelFromRepo == null)
            {
                return NotFound();
            }

            if(Status==0){
              groupModelFromRepo.Users=groupModelFromRepo.Users+"#"+UserId;
              
            
            }else
            {
                 groupModelFromRepo.Admins=groupModelFromRepo.Admins+"#"+UserId;
            }


           

          
          await  _context.SaveChangesAsync();

            return Ok(groupModelFromRepo);

        }

    }



}