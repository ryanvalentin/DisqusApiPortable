﻿using System;

namespace Disqus.Api.V30
{
    public class DsqApiException : Exception
    {
        public DsqApiException(string message, int? code = null) 
            : base(message)
        {
            this.Code = code;
        }

        public int? Code { get; set; }
    }
}