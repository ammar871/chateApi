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


    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {




        private readonly DBContext _context;
        private IMapper _mapper;
        public UserController(IMapper mapper, DBContext context)
        {

            _mapper = mapper;
            _context = context;

        }



        [HttpGet]
        [Route("get-Users")]
        public async Task<ActionResult> GetAll([FromForm]PagingParameterModel  @params,[FromForm] string userId)
        {
            var data = await _context.Users.Where(x=> x.Role=="user"&&x.Id!=userId).ToListAsync();

          List<UserDetailResponse> users=new List<UserDetailResponse>();

          foreach(var item in data){
           UserDetailResponse userDetails= _mapper.Map<UserDetailResponse>(item);
           users.Add(userDetails);

          }

          var paginationMetadata = new PaginationMetadata(users.Count(), @params.Page, @params.ItemsPerPage);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            var items =  users.Skip((@params.Page - 1) * @params.ItemsPerPage)
                                       .Take(@params.ItemsPerPage);
         return Ok(new {

             items=items,
             currentPage=@params.Page,
              totalPage=paginationMetadata.TotalPages
    });
        }





    }





}