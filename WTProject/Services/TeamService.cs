using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WTProject.model;

namespace WTProject.Services
{
    public class TeamService
    {
        public TeamService()
        { }

        //public TeamService(IWebHostEnvironment webHostEnvironment)
        //{
        //    WebHostEnvironment = webHostEnvironment;
        //}

        //public IWebHostEnvironment WebHostEnvironment { get; }

        public IEnumerable<Team.Student> GetStudents(string team)
        {
            Team teamRef = new Team(team);
            foreach (Team.Student item in teamRef.Deserialize().StudentInfos)
            {
                yield return item;
            }
        }
    }
}
