﻿
using System;
namespace encuesta
{
    public class Usuario : BaseItem
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{ID}, {Username}";
        }
    }
}