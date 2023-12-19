using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Kwizzez.DAL.Dtos.Answers;
using Kwizzez.DAL.Dtos.Questions;
using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.Dtos.Responses;
using Kwizzez.DAL.Dtos.StudentScores;
using Kwizzez.DAL.Services.Answers;
using Kwizzez.DAL.Services.Quizzes;
using Kwizzez.DAL.Services.StudentScores;
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
        public readonly IStudentScoresService _studentScoresService;
        public readonly IAnswersService _answersService;
        public readonly IMapper _mapper;

        public QuizzesController(IQuizzesService quizzesService, IStudentScoresService studentScoresService, IAnswersService answersService, IMapper mapper)
        {
            _quizzesService = quizzesService;
            _studentScoresService = studentScoresService;
            _answersService = answersService;
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
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var quiz = _quizzesService.GetQuizById(id, studentId);

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
        [Authorize(Roles = $"{Roles.Student}")]
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
            var quizExists = _quizzesService.QuizExists(id);

            if (!quizExists)
                return NotFound();

            _quizzesService.DeleteQuiz(id);
            return NoContent();
        }

        [HttpPost("StartQuiz/{id}")]
        [Authorize(Roles = $"{Roles.Student}")]
        public IActionResult StartQuiz(string id) {
            var quizExists = _quizzesService.QuizExists(id);

            if (!quizExists)
                return NotFound();

            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentScoreId = _studentScoresService.GetStudentScoreId(studentId, id);

            if (studentScoreId != null)
                return BadRequest();

            _quizzesService.StartQuiz(id, studentId);
            return Ok();
        }

        [HttpGet("GetAnswers/{id}")]
        [Authorize(Roles = $"{Roles.Student}")]
        public ActionResult<ApiResponse<StudentScoreAnswersDto>> GetAnswers(string id) {
            var quizExists = _quizzesService.QuizExists(id);

            if (!quizExists)
                return NotFound();

            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentScoreId = _studentScoresService.GetStudentScoreId(studentId, id);

            if (studentScoreId == null)
                return Forbid();

            return new ApiResponse<StudentScoreAnswersDto>() {
                Data = _studentScoresService.GetStudentScoreAnswersById(studentScoreId)
            };
        }

        [HttpPost("SelectAnswer/{id}")]
        [Authorize(Roles = $"{Roles.Student}")]
        public IActionResult SelectAnswer(string id, SelectAnswerDto answerDto) {
            // Check quiz existance
            var quizExists = _quizzesService.QuizExists(id);

            if (!quizExists)
                return NotFound();

            // Check if student started this quiz before
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentScoreId = _studentScoresService.GetStudentScoreId(studentId, id);

            if (studentScoreId == null)
                return BadRequest();

            // Check answer existance
            var answerExists = _answersService.AnswerExists(answerDto.AnswerId);

            if (!answerExists)
                return NotFound();

            // Check that answer belongs to this quiz
            var answerRelatedToQuiz = _quizzesService.AnswerRelatedToQuiz(id, answerDto.AnswerId);

            if (!answerRelatedToQuiz)
                return BadRequest();

            _studentScoresService.SelectAnswer(answerDto.AnswerId, studentScoreId);

            return Ok();
        }
    }
}