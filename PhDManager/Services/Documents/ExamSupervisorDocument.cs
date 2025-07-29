using Microsoft.JSInterop;
using PhDManager.Models.Documents;

namespace PhDManager.Services.Documents;

public class ExamSupervisorDocument(IJSRuntime jsRuntime, EnumService enumService, ExamSupervisor examSupervisor) : DocumentTemplate(jsRuntime, enumService)
{
    protected override string DocumentName => examSupervisor.Student.User.DisplayName + "_ds_skolitel";
    protected override string TemplatePath => Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "exam_supervisor_template.docx");
    protected override Dictionary<string, List<string?>> GetReplacements()
    {
        return new Dictionary<string, List<string?>>
        {
            {"{Student}", [examSupervisor.Student.User.UserName] },
            {"{Supervisor}", [examSupervisor.Student.Thesis?.Supervisor.User.DisplayName] },
            {"{Opponent}", [examSupervisor.OpponentDisplayName] },
            {"{OpponentMail}", [examSupervisor.OpponentMail] },
            {"{OpponentPhoneNumber}", [examSupervisor.OpponentPhoneNumber] },
            {"{OpponentDepartment}", [examSupervisor.OpponentDepartment] },
            {"{Examiner}", [examSupervisor.Examiner.User.DisplayName] },
            {"{Chairperson}", [examSupervisor.Chairperson.User.DisplayName] },
            {"{ChairpersonDepartment}", [examSupervisor.Chairperson.Department?.Code] },
            {"{ChairpersonMail}", [examSupervisor.Chairperson.User.Email] },
            {"{ChairpersonPhoneNumber}", [examSupervisor.Chairperson.User.PhoneNumber] },
            {"{ExternalMember}", [examSupervisor.ExternalMember.User.DisplayName] },
            {"{ExternalMemberDepartment}", [examSupervisor.ExternalMember.Department?.Code] },
            {"{ExternalMemberMail}", [examSupervisor.ExternalMember.User.Email] },
            {"{ExternalMemberPhoneNumber}", [examSupervisor.ExternalMember.User.PhoneNumber] },
            {"{AcademicCommitteeMember}", [examSupervisor.AcademicCommitteeMember.User.DisplayName] },
            {"{AcademicCommitteeMemberDepartment}", [examSupervisor.AcademicCommitteeMember.Department?.Code] },
            {"{AcademicCommitteeMemberMail}", [examSupervisor.AcademicCommitteeMember.User.Email] },
            {"{AcademicCommitteeMemberPhoneNumber}", [examSupervisor.AcademicCommitteeMember.User.PhoneNumber] },
            {"{AdditionalMember}", [examSupervisor.AdditionalMember.User.DisplayName] },
            {"{AdditionalMemberDepartment}", [examSupervisor.AdditionalMember.Department?.Code] },
            {"{AdditionalMemberMail}", [examSupervisor.AdditionalMember.User.Email] },
            {"{AdditionalMemberPhoneNumber}", [examSupervisor.AdditionalMember.User.PhoneNumber] },
            {"{CurrentDate}", [examSupervisor.CurrentDate.ToString("dd.MM.yyyy")] }
        };
    }
}