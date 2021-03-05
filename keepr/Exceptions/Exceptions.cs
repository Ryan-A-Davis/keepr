using System;

namespace keepr.Exceptions
{
  public class NotAuthorized : Exception
  {
    public NotAuthorized(string message) : base(message)
    {
    }
  }
}