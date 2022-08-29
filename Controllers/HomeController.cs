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


    [Route("home")]
    [ApiController]
    public class HomeController : ControllerBase
    {




        private readonly DBContext _context;
        private IMapper _mapper;
        public HomeController(IMapper mapper, DBContext context)
        {

            _mapper = mapper;
            _context = context;

        }



    //     [HttpGet]
    //     [Route("get-home")]
    //     public async Task<ActionResult> GetAll([FromQuery]PagingParameterModel  @params)
    //     {
    //         var data = await _context.Rooms.ToListAsync();
    //       var paginationMetadata = new PaginationMetadata(data.Count(), @params.Page, @params.ItemsPerPage);
    //         Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

    //         var items =  data.Skip((@params.Page - 1) * @params.ItemsPerPage)
    //                                    .Take(@params.ItemsPerPage);
    //      return Ok(new {

    //          items=items,
    //          currentPage=@params.Page,
    //           totalPage=paginationMetadata.TotalPages
    // });
    //     }





    }





}