using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WTProject.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using WTProject.Services;
using System.IO;

namespace WTProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private TeamService teamService;

        public IEnumerable<Team.Student> GetStudentInfos { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, TeamService teamService)
        {
            _logger = logger;
            this.teamService = teamService;
        }

        public void OnGet()
        {
            var studentInfo1 = new Team.Student()
            {
                ImgURL = "favicon.ico",
                No = "201813709004",
                Name = "Atadan",
                Surname = "İçen",
                Age = 21,
                Course = new string[] { "Bilgisayar Ağları", "Bilgisayar Organizasyonu", "İş Sağlığı ve Güvenliği" }
            };

            var studentInfo2 = new Team.Student()
            {
                ImgURL = "favicon.ico",
                No = "201813709033",
                Name = "Mert",
                Surname = "Çikendin",
                Age = 20,
                Course = new string[] { "Bilgisayar Ağları", "Bilgisayar Organizasyonu", "İş Sağlığı ve Güvenliği" }
            };

            Team team = new Team("nesneye yonelik programlama");
            team.Extra = "11/11/2020";
            team.StudentInfos.Add(studentInfo1);
            team.StudentInfos.Add(studentInfo2);
            team.Serialize();

            GetStudentInfos = team.Deserialize().StudentInfos; //teamService.GetStudents("web tasarimi");
        }
    }
}
