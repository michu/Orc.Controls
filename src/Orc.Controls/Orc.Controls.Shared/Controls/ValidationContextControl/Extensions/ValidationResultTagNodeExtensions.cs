﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationResultTagNodeExtensions.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Controls
{
    using System.Collections.Generic;
    using System.Linq;
    using Catel;
    using Catel.Data;

    internal static class ValidationResultTagNodeExtensions
    {
        public static void AddValidationResultTypeNode(this ValidationResultTagNode validationResultTagNode, IValidationContext validationContext,
            ValidationResultType validationResultType, IValidationNamesService validationNamesService)
        {
            Argument.IsNotNull(() => validationResultTagNode);
            Argument.IsNotNull(() => validationContext);
            Argument.IsNotNull(() => validationNamesService);

            var validationResults = validationNamesService.GetCachedResultsByTagName(validationResultTagNode.TagName).Where(x => x.ValidationResultType == validationResultType);

            var validationResultsList = validationResults as IList<IValidationResult> ?? validationResults.ToList();
            if (!validationResultsList.Any())
            {
                return;
            }

            var resultTypeNode = new ValidationResultTypeNode(validationResultType, validationResultsList, validationNamesService);
            validationResultTagNode.Children.Add(resultTypeNode);
        }
    }
}