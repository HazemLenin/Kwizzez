using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.Responses;
using Kwizzez.DAL.Dtos.StudentScores;
using Kwizzez.DAL.Services.Quizzes;
using Kwizzez.DAL.Services.StudentScores;
using Kwizzez.DAL.Utilities;
using Kwizzez.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kwizzez.Api.Controllers
{
    [ApiController]
    [Route("api/Quizzes/{quizId}/[controller]")]
    [Authorize]
    public class StudentScoresController : Controller
    {
        private readonly IStudentScoresService _studentScoresService;
        private readonly IQuizzesService _quizzesService;

        public StudentScoresController(IStudentScoresService studentScoresService, IQuizzesService quizzesService)
        {
            _studentScoresService = studentScoresService;
            _quizzesService = quizzesService;
        }

        // GET: api/StudentScores
        [HttpGet]
        public ActionResult<ApiPaginatedResponse<PaginatedList<StudentScoreDto>>> GetStudentScores(int pageNumber = 1, int pageSize = 10)
        {
            var scores = _studentScoresService.GetPaginatedStudentScore(pageNumber, pageSize);

            return new ApiPaginatedResponse<PaginatedList<StudentScoreDto>>()
            {
                Data = scores,
                PageIndex = scores.PageIndex,
                TotalPages = scores.TotalPages,
                TotalCount = scores.TotalCount,
                HasNext = scores.HasNext,
                HasPrevious = scores.HasPrevious
            };
        }

        // GET: api/StudentScores/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Teacher}")]
        public ActionResult<ApiResponse<StudentScoreDto>> GetStudentScore([FromRoute] string quizId, string id)
        {
            if (!_quizzesService.QuizExists(quizId))
                return NotFound();

            var studentScore = _studentScoresService.GetStudentScoreById(id);

            return new ApiResponse<StudentScoreDto>()
            {
                Data = studentScore
            };
        }

        // // PUT: api/StudentScores/{id}
        // [HttpPut("{id}")]
        // [Authorize(Roles = Roles.Teacher)]
        // public IActionResult PutStudentScore([FromRoute] string quizId, [FromRoute] string id, StudentScoreFormDto studentScoreFormDto)
        // {
        //     if (!QuizExists(quizId))
        //         return NotFound();
        //     var studentScoreDto = _mapper.Map<StudentScoreDto>(studentScoreFormDto);
        //     studentScoreDto.TeacherId = User.Identity.Name;
        //     studentScoreDto.Score = studentScoreDto.Questions.Sum(q => q.Degree);

        //     _studentScoresService.UpdateStudentScore(studentScoreDto);

        //     return NoContent();
        // }

        // // POST: api/StudentScores
        // [HttpPost]
        // [Authorize(Roles = Roles.Teacher)]
        // public ActionResult<ApiResponse<StudentScoreDto>> PostStudentScore([FromRoute] string quizId, StudentScoreFormDto studentScoreFormDto)
        // {
        //     if (!QuizExists(quizId))
        //         return NotFound();
        //     var studentScoreDto = _mapper.Map<StudentScoreDto>(studentScoreFormDto);
        //     studentScoreDto.TeacherId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //     studentScoreDto.Score = studentScoreDto.Questions.Sum(q => q.Degree);
        //     _studentScoresService.AddStudentScore(studentScoreDto);

        //     return CreatedAtAction(nameof(GetStudentScore), new { id = studentScoreDto.Id }, studentScoreDto);
        // }


        // DELETE: api/StudentScores/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = $"{Roles.Admin},{Roles.Teacher}")]
        public IActionResult DeleteStudentScore([FromRoute] string quizId, string id)
        {
            if (!_quizzesService.QuizExists(quizId))
                return NotFound();

            var studentScoreDto = _studentScoresService.GetStudentScoreById(id);

            if (studentScoreDto == null)
                return NotFound();

            _studentScoresService.DeleteStudentScore(studentScoreDto);
            return NoContent();
        }
    }
}