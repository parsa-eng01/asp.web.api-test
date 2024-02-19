namespace TestProject.Web.Core;

public class Result<TValue>
{
  public Result(TValue value)
  {
    IsSuccess = true;
    Error = string.Empty;
    Value = value;
  }
  public Result(string error)
  {
    IsSuccess = false;
    Error = error;
    Value = default;
  }


  public bool IsSuccess { get; set; }
  public string Error { get; set; }
  public TValue? Value { get; set; }

  public static implicit operator Result<TValue>(string error) => new(error);
  public static implicit operator Result<TValue>(TValue value) => new(value);
}
