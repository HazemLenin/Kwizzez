using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Kwizzez.DAL.Dtos.Questions;
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

        // GET: api/Quizzes/MyQuizzes
        [HttpGet("MyQuizzes")]
        [Authorize(Roles = Roles.Teacher)]
        public ActionResult<ApiPaginatedResponse<PaginatedList<QuizDto>>> MyQuizzes(int pageNumber = 1, int pageSize = 10)
        {
            var quizzes = _quizzesService.GetPaginatedUserQuizzes(User.FindFirstValue(ClaimTypes.NameIdentifier), pageNumber, pageSize);

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
        [Authorize(Roles = Roles.Student)]
        public ActionResult<ApiResponse<QuizDto>> GetQuiz(string id)
        {
            var quiz = _quizzesService.GetQuizById(id);

            if (quiz == null)
                return NotFound();

            return new ApiResponse<QuizDto>()
            {
                Data = quiz
            };
        }

        // GET: api/Quizzes/GetQuizDetails/{id}/
        [HttpGet("GetQuizDetails/{id}")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Teacher}")]
        public ActionResult<ApiResponse<QuizDetailedDto>> GetQuizDetails(string id)
        {
            var quiz = _quizzesService.GetDetailedQuizById(id);

            if (quiz == null)
                return NotFound();

            return new ApiResponse<QuizDetailedDto>()
            {
                Data = quiz
            };
        }

        // GET: api/Quizzes/GetQuizQuestions/{id}/
        [HttpGet("GetQuizQuestions/{id}")]
        public ActionResult<ApiResponse<List<QuestionForStudentDto>>> GetQuizQuestions(string id)
        {
            var questions = _quizzesService.GetQuizQuestionsById(id);

            if (questions == null)
                return NotFound();

            return new ApiResponse<List<QuestionForStudentDto>>()
            {
                Data = questions
            };
        }

        // PUT: api/Quizzes/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Teacher)]
        public IActionResult PutQuiz(string id, EditQuizDto quizEditDto)
        {
            if (!_quizzesService.QuizExists(id))
                return NotFound();

            _quizzesService.UpdateQuiz(quizEditDto);

            return NoContent();
        }

        // POST: api/Quizzes
        [HttpPost]
        [Authorize(Roles = Roles.Teacher)]
        public ActionResult<ApiResponse<QuizDto>> PostQuiz(AddQuizDto quizAddDto)
        {
            var quizId = _quizzesService.AddQuiz(quizAddDto, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return CreatedAtAction(nameof(GetQuiz), new { id = quizId }, new ApiResponse<QuizDto>() {
                Data = _mapper.Map<QuizDto>(quizAddDto)
            });
        }


        // DELETE: api/Quizzes/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Teacher}")]
        public IActionResult DeleteQuiz(string id)
        {
            var QuizDetailedDto = _quizzesService.QuizExists(id);

            if (!QuizDetailedDto)
                return NotFound();

            _quizzesService.DeleteQuiz(id);
            return NoContent();
        }
    }
}