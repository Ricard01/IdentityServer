using Identity.Domain;

namespace Identity.Infrastructure.Common.Models;

public class Result
{
    internal Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    //return for Create and Update | Post and PAtch 

    public string? UserId { get; set; }
    public string? Rol { get; set; }
    public string? Nombre { get; set; }

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    public static Result Success()
    {
        return new Result(true, Array.Empty<string>());
    }

    public static Result Failure(IEnumerable<string> errors)
    {
        return new Result(false, errors);
    }

    //return for Create and Update | Post and PAtch 
    public static Result Success(ApplicationUser user, string rol)
    {
        return new Result(true, new string[] { }) { UserId = user.Id, Nombre = user.Nombre, Rol = rol };
    }
}
