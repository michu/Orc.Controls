﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationContextControlViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls.Examples.ViewModels
{
    using System.Threading.Tasks;
    using Catel.Data;
    using Catel.MVVM;

    public class ValidationContextControlViewModel : ViewModelBase
    {
        public IValidationContext ValidationContext { get; set; }

        protected override Task InitializeAsync()
        {
            var context = new ValidationContext();

            var result1 = BusinessRuleValidationResult.CreateErrorWithTag("Error1 message", "Rule1");
            var result2 = BusinessRuleValidationResult.CreateWarningWithTag("Warning1 message", "Rule1");
            var result3 = FieldValidationResult.CreateWarningWithTag("Property1","Warning2 message", "Rule1");

            var result4 = BusinessRuleValidationResult.CreateErrorWithTag("Error2 message", "Rule2");
            var result5 = BusinessRuleValidationResult.CreateErrorWithTag("Error3 message", "Rule2");

            var result6 = BusinessRuleValidationResult.CreateError("Error3 message");
            var result7 = BusinessRuleValidationResult.CreateError("Error4 message");
            var result8 = FieldValidationResult.CreateWarning("Property2", "Warning3 message");

            context.AddBusinessRuleValidationResult(result1);
            context.AddBusinessRuleValidationResult(result2);
            context.AddFieldValidationResult(result3);
            context.AddBusinessRuleValidationResult(result4);
            context.AddBusinessRuleValidationResult(result5);
            context.AddBusinessRuleValidationResult(result6);
            context.AddBusinessRuleValidationResult(result7);
            context.AddFieldValidationResult(result8);


            ValidationContext = context;

            return base.InitializeAsync();
        }
    }
}