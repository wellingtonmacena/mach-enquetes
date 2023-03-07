using MachEnquetes.Entities;
using MachEnquetes.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachEnquetes.Repositories
{
    public class SurveyRepository : Controller
    {
        private MachEnquetesContext MachEnquetesContext;

        public SurveyRepository(MachEnquetesContext machEnquetesContext)
        {
            this.MachEnquetesContext = machEnquetesContext;
        }

        public async Task<ObjectResult> GetAll()
        {
            try
            {
                var surveys = await MachEnquetesContext.Surveys.ToListAsync();
                if (surveys.Count == 0)
                    return StatusCode(204, surveys);

                return StatusCode(200, surveys);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> GetAllSurveysFromUserId(int id)
        {
            try
            {
                var surveys =  await MachEnquetesContext.Surveys.Where(item => item.FKCreatorUser == id).ToListAsync();
                if (surveys.Count == 0)
                    return StatusCode(204, surveys);

                return StatusCode(200, surveys);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> GetById(int id)
        {
            try
            {
                var surveys = await MachEnquetesContext.Surveys.Where(s => s.Id == id).ToListAsync();
                if (surveys == null || surveys.Count == 0)
                    return StatusCode(404, surveys);

                return StatusCode(200, surveys.First());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> Create(Survey survey)
        {
            try
            {
                var foundSurvey = await GetById(survey.Id);
                if (foundSurvey.StatusCode == 404)
                {
                    MachEnquetesContext.Surveys.AddAsync(survey);
                    await MachEnquetesContext.SaveChangesAsync();
                    return StatusCode(201, survey);
                }

                return StatusCode(406, new { Message = "Already exists " });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> Update(int id, Survey updatedSurvey)
        {
            try
            {
                var foundSurvey = MachEnquetesContext.Surveys.Where(s => s.Id == id);

                if (foundSurvey == null || foundSurvey.Count() == 0)
                    return StatusCode(404, null);

                var survey = foundSurvey.First();

                survey.Title = updatedSurvey.Title;
                survey.CanUserUpdateVote = updatedSurvey.CanUserUpdateVote;
                survey.CanUnregistredVote = updatedSurvey.CanUnregistredVote;
                survey.FinishDate = updatedSurvey.FinishDate;
                survey.OptionsSelectedCount = updatedSurvey.OptionsSelectedCount;
                survey.VotesCount = updatedSurvey.VotesCount;
                survey.LastModifiedDate = DateTime.UtcNow;

                MachEnquetesContext.Surveys.Update(survey);
                await MachEnquetesContext.SaveChangesAsync();

                return StatusCode(200, survey);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> DeleteById(int id)
        {
            try
            {
                var foundSurvey = await GetById(id);
                if (foundSurvey.StatusCode == 200)
                {
                    _ = MachEnquetesContext.Surveys.Remove((Survey)foundSurvey.Value);
                    await MachEnquetesContext.SaveChangesAsync();

                    return StatusCode(200, null);
                }

                return StatusCode(404, new { Message = "User not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }

        public async Task<ObjectResult> DeleteAll()
        {
            try
            {
                var surveys = MachEnquetesContext.Surveys;
                foreach (var item in surveys)
                {
                    MachEnquetesContext.Surveys.Remove(item);
                };

                await MachEnquetesContext.SaveChangesAsync();

                return StatusCode(204, null);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ex.Message });
            }
        }


    }
}
