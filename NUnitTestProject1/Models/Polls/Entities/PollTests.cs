﻿using System;
using System.Collections.Generic;
using MainMVC.Models.Polls;
using MainMVC.Models.Polls.Entities;
using NUnit.Framework;

namespace NUnitTestProject1.Models.Polls.Entities
{
    [TestFixture()]
    public class PollTests
    {
        [Test()]
        public void UpdateTest()
        {
            var db = new RAM_MemoryRepository();
            var oldPoll = (Poll)db.GetPoll(1).Clone();

            var newPoll = new Poll()
            {
                Id = 2,
                Name = "Second Poll",
                CreatorLogin = "KoAmar",
                Description = "Miusov, as a man man of breeding and deilcacy, could not but feel some inwrd qualms, when he reached the Father Superior's with Ivan: he felt ashamed of havin lost his temper. He felt that he ought to have disdaimed that despicable wretch, Fyodor Pavlovitch, too much to have been upset by him in Father Zossima's cell, and so to have forgotten himself. Teh monks were not to blame, in any case, he reflceted, on the steps. And if they're decent people here (and the Father Superior, I understand, is a nobleman) why not be friendly and courteous withthem? I won't argue, I'll fall in with everything, I'll win them by politness, and show them that I've nothing to do with that Aesop, thta buffoon, that Pierrot, and have merely been takken in over this affair, just as they have.",
                CreationDate = default,
                QuestionsCount = 1,
                Questions = new List<Question>(){
                    new Question(){
                        Id = 3,
                        Text = "Is this the real lifeee?",
                        SoleAnswer = true,
                        AnswersCount = 4,
                        PossibleAnswers = new List<Answer>()
                        {
                            new Answer("Is this just fantasy6?"){Id = 6 },
                            new Answer("I'm just a poor boy"){Id = 7 },
                            new Answer("I don't wanna die"){Id = 8 },
                            new Answer("I don't wanna die"){Id = 9 }
                        }
                    }
                },
            };
            db.Update(newPoll);

            var expected = newPoll.Questions[0];
            var result = db.GetPoll(2).Questions[0];

            Console.WriteLine(expected.Id);
            Console.WriteLine(expected.Text);
            Console.WriteLine(expected.PossibleAnswers[0].Text);
            Console.WriteLine(expected);
            Console.WriteLine(result.Id);
            Console.WriteLine(result.Text);
            Console.WriteLine(result.PossibleAnswers[0].Text);
            Console.WriteLine(result);

            //Assert.AreEqual(db.GetQuestion(1).PossibleAnswers.Count, old.PossibleAnswers.Count);
            Assert.AreEqual(expected.Text, result.Text);
        }
    }
}