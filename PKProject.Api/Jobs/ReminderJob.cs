using PKProject.Domain.IRepositories;
using PKProject.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.Jobs
{
    public class ReminderJob
    {
        private readonly ICardRepository _repository;
        private readonly IEmailSender _emailSender;

        public ReminderJob(ICardRepository repository, IEmailSender emailSender)
        {
            _repository = repository;
            _emailSender = emailSender;
        }

        public async Task PrepareRemind()
        {
            var dateTimeNow = DateTime.Now;
            dateTimeNow = dateTimeNow.AddHours(24);

            var cards = await _repository.GetCardsByDeadlineDateIn24H(dateTimeNow);

            foreach (var card in cards)
            {
                var user = card.UserEmail;
                var cardTitle = card.Title;
                var cardDescription = card.Description;
                var cardDeadlineDate = card.DeadlineDate;

                string subject = $"Za 24 godziny mija termin zadania {cardTitle}.";
                string toUserEmail = user;
                string text = $"Cześć!<br />" +
                                $"Pozostało dokładnie 24 godziny do terminu wykonania zadania <b>{cardTitle}</b> {cardDeadlineDate}. <br />" +
                                $"Opis karty: {cardDescription} <br />" +
                                $"Zobacz zmiany w aplikacji <a href='#'>PkProjectApp</a> <br />" +
                                $"Zespół PkProjectApp <br />" +
                                $"Miłego dnia!";

                await _emailSender.SendEmail(subject, toUserEmail, text);
            }
        }
    }
}
