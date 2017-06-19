namespace API.DataLogic.ViewModels
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The response status describes the outcome of an operation in detail
    /// </summary>
    public class SubmissionStatus
    {
        public SubmissionStatusCode StatusCode { get; set; }

        public List<string> Messages { get; set; } = new List<string>();
    }

    /// <summary>
    /// Status codes for the submission status
    /// </summary>
    public enum SubmissionStatusCode
    {
        Success = 1,
        Warning = 2,
        Failure = 3
    }
}