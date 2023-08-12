using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.Dtos.Responses;
using Kwizzez.DAL.Services.Quizzes;
using Kwizzez.DAL.Utilities;
using Kwizzez.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Kwizzez.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class QuizzesController : Controller
    {
        public readonly IQuizzesService _quizzesService;
        public readonly IMapper _mapper;

        public QuizzesController(IQuizzesService quizzesService, IMapper mapper)
        {
            _quizzesService = quizzesService;
            _mapper = mapper;
        }

        // GET: api/Quizzes
        [HttpGet]
        public ActionResult<ApiPaginatedResponse<PaginatedList<QuizDto>>> GetQuizzes(int pageNumber = 1, int pageSize = 10)
        {
            var quizzes = _quizzesService.GetPaginatedQuizzes(pageNumber, pageSize);

            return new ApiPaginatedResponse<PaginatedList<QuizDto>>()
            {
                Data = quizzes,
                PageIndex = quizzes.PageIndex,
                TotalPages = quizzes.TotalPages,
                TotalCount = quizzes.TotalCount,
                HasNext = quizzes.HasNext,
                HasPrevious = quizzes.HasPrevious
            };
        }

        // GET: api/Quizzes/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Teacher}")]
        public ActionResult<ApiResponse<QuizDto>> GetQuiz(string id)
        {
            var quiz = _quizzesService.GetQuizById(id);

            return new ApiResponse<QuizDto>()
            {
                Data = quiz
            };
        }

        // GET: api/Quizzes/GetQuizByCode
        [HttpGet("GetQuizByCode")]
        [Authorize(Roles = Roles.Student)]
        public ActionResult<ApiResponse<QuizDto>> GetQuizByCode(int code)
        {
            var quiz = _quizzesService.GetQuizByCode(code);

            return new ApiResponse<QuizDto>()
            {
                Data = quiz
            };
        }

        // PUT: api/Quizzes/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Teacher)]
        public IActionResult PutQuiz(string id, QuizFormDto quizFormDto)
        {
            var quizDto = _mapper.Map<QuizDto>(quizFormDto);
            quizDto.TeacherId = User.Identity.Name;
            quizDto.Score = quizDto.Questions.Sum(q => q.Degree);

            _quizzesService.UpdateQuiz(quizDto);

            return NoContent();
        }

        // POST: api/Quizzes
        [HttpPost]
        [Authorize(Roles = Roles.Teacher)]
        public ActionResult<ApiResponse<QuizDto>> PostQuiz(QuizFormDto quizFormDto)
        {
            var quizDto = _mapper.Map<QuizDto>(quizFormDto);
            quizDto.TeacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            quizDto.Score = quizDto.Questions.Sum(q => q.Degree);
            _quizzesService.AddQuiz(quizDto);

            return CreatedAtAction(nameof(GetQuiz), new { id = quizDto.Id }, quizDto);
        }


        // DELETE: api/Quizzes/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Teacher}")]
        public IActionResult DeleteQuiz(string id)
        {
            var quizDto = _quizzesService.GetQuizById(id);

            if (quizDto == null)
                return NotFound();

            _quizzesService.DeleteQuiz(quizDto);
            return NoContent();
        }
    }
}