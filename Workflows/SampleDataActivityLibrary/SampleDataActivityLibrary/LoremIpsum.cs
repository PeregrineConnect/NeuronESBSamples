using System;
using System.Activities;
using System.Activities.PeregrineAttributes;
using System.Activities.Validation;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace SampleDataActivityLibrary
{
	/// <summary>
	///     Activity that generates lorem ipsum text.
	/// </summary>
	[DisplayName("Lorem Ipsum")]
    [CustomIcon("Image1")]
    [ResourceTypeName(typeof(Resource1))]
	[Description("Generates Lorem Ipsum text based on the specified criteria.")]
	public sealed class LoremIpsum : CodeActivity<string>
    {
        public LoremIpsum()
        {
            MinWords = 5;
            MaxWords = 10;
            MinSentences = 3;
            MaxSentences = 6;
            Paragraphs = 1;
        }

		// Design-time properties
		[Category("General")]
		[Description("Specifies the maximum number of sentences in a paragraph.")]
		public int MaxSentences { get; set; }
		
        [Category("General")]
		[Description("Specifies the maximum number of words in a sentence.")]
		public int MaxWords { get; set; }
		
        [Category("General")]
		[Description("Specifies the minimum number of sentences in a paragraph.")]
		public int MinSentences { get; set; }
		
        [Category("General")]
		[Description("Specifies the minimum number of words in a sentence.")]
		public int MinWords { get; set; }
		
        [Category("General")]
		[Description("Specifies the number of paragraphs to generate.")]
		public int Paragraphs { get; set; }

		[Category("General")]
		[Editor("ExpressionEditor", "UITypeEditor")]
		[Description("The output text containing the generated Lorem Ipsum content.")]
		public new OutArgument<string> Result
		{
			get
			{
				return base.Result;
			}
			set
			{
				base.Result = value;
			}
		}


		/// <summary>
		///     Creates and validates a description of the activity’s arguments, variables, child activities, and activity
		///     delegates.
		/// </summary>
		/// <param name="metadata">
		///     The activity’s metadata that encapsulates the activity’s arguments, variables, child activities,
		///     and activity delegates.
		/// </param>
		protected override void CacheMetadata(CodeActivityMetadata metadata)
        {
            base.CacheMetadata(metadata);

            // validate properties
            if (MinWords < 1)
            {
                metadata.AddValidationError(new ValidationError("MinWords must be at least 1."));
            }

            if (MaxWords < 1)
            {
                metadata.AddValidationError(new ValidationError("MaxWords must be at least 1."));
            }

            if (MinSentences < 1)
            {
                metadata.AddValidationError(new ValidationError("MinSentences must be at least 1."));
            }

            if (MaxSentences < 1)
            {
                metadata.AddValidationError(new ValidationError("MaxSentences must be at least 1."));
            }

            if (Paragraphs < 1)
            {
                metadata.AddValidationError(new ValidationError("Paragraphs must be at least 1."));
            }

            if (MaxWords < MinWords)
            {
                metadata.AddValidationError(new ValidationError("MaxWords must be greater than or equal to MinWords."));
            }

            if (MaxSentences < MinSentences)
            {
                metadata.AddValidationError(
                    new ValidationError("MaxSentences must be greater than or equal to MinSentences."));
            }
        }

        /// <summary>
        ///     The code that is executed when the workflow activity is run.
        /// </summary>
        /// <param name="context">The execution context under which the activity executes.</param>
        /// <returns>The lorem ipsum text.</returns>
        protected override string Execute(CodeActivityContext context)
        {
            var text = GeneateLoremIpsum(MinWords, MaxWords, MinSentences, MaxSentences, Paragraphs);
            return text;
        }


        /// <summary>
        ///     Generates Lorem Ipsum text base on the generation criteria specified.
        /// </summary>
        /// <param name="minWords">The minimum number of words.</param>
        /// <param name="maxWords">The maximum number of words.</param>
        /// <param name="minSentences">The minimum number of sentences.</param>
        /// <param name="maxSentences">The maximum number of sentences.</param>
        /// <param name="numParagraphs">The number of paragraphs to generate.</param>
        /// <returns>The lorem ipsum text.</returns>
        private static string GeneateLoremIpsum(int minWords, int maxWords, int minSentences, int maxSentences,
            int numParagraphs)
        {
            var words = new[]
            {
                "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat"
            };

            var rand = new Random();

            var sb = new StringBuilder();

            for (int p = 0; p < numParagraphs; p++)
            {
                int numSentences = rand.Next(minSentences, maxSentences + 1);

                if (p > 0)
                {
                    sb.AppendLine();
                    sb.AppendLine();
                }

                for (int s = 0; s < numSentences; s++)
                {
                    int numWords = rand.Next(minWords, maxWords + 1);

                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0)
                        {
                            sb.Append(" ");
                        }

                        if (w == 0)
                        {
                            var word = words[rand.Next(words.Length)];

                            sb.Append(word[0].ToString(CultureInfo.InvariantCulture).ToUpperInvariant() +
                                      word.Substring(1));
                        }
                        else
                        {
                            sb.Append(words[rand.Next(words.Length)]);
                        }
                    }
                    sb.Append(". ");
                }
            }

            return sb.ToString();
        }
    }
}