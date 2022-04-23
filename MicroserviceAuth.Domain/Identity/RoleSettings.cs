namespace MicroserviceAuth.Domain.Identity;

/// <summary>
/// Available roles in the system
/// </summary>
public class RoleSettings
{
    public const string EGRESS = "EGRESS";
    public const string TEACHER = "TEACHER";
    public const string STUDENT = "STUDENT";
    public const string COLLEGIATE = "COLLEGIATE";
    public const string DEPARTMENT = "DEPARTMENT";
    public const string ADMINISTRATOR = "ADMINISTRATOR";

    public const string SECTION_CLAIM_NAME = "SECTION_ID";

    public static string[] AllRoles
    {
        get => new[]
        {
            EGRESS,
            TEACHER,
            STUDENT,
            COLLEGIATE,
            DEPARTMENT,
            ADMINISTRATOR
        };
    }

    public static string[] ManagerRoles
    {
        get => new[]
        {
            COLLEGIATE,
            DEPARTMENT,
            ADMINISTRATOR
        };
    }

    public static string[] NormalUserRoles
    {
        get => new[]
        {
            EGRESS,
            TEACHER,
            STUDENT
        };
    }
}