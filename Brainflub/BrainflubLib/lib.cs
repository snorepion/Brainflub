using System;
using System.Text;
namespace Brainflub
{

    /// <summary>
    /// Provides parsing for Brainflub scripts. This class cannot be inherited.
    /// </summary>
    public sealed class BrainflubParser
    {
        /// <summary>
        /// The current value of the accumulator.
        /// </summary>
        public int Accumulator { get; set; }
        /// <summary>
        /// The next character to display.
        /// </summary>
        private char NextCharacter { get; set; }
        /// <summary>
        /// Used to build the output text.
        /// </summary>
        private StringBuilder OutputContents = new StringBuilder();
        /// <summary>
        /// The position that the parser is currently at.
        /// </summary>
        public int CodePosition { get; set; }
        /// <summary>
        /// User input (taken by using space).
        /// </summary>
        private string UserInput { get; set; }
        /// <summary>
        /// After parsing, results will be found in this string.
        /// </summary>
        public string Output { get; set; }
        /// <summary>
        /// Parses Brainflub code by iterating through each character, sending output to the BrainflubParser.Output property.
        /// </summary>
        /// <param name="pos">The position to start at (defaults to zero).</param>
        /// <param name="acc">Accumulator position to start at (defaults to zero).</param>
        public void Parse(string Code, int pos = 0, int acc = 0)
        {
            Accumulator = acc;
            CodePosition = pos;
            int i = pos;
            while (i == i)
            {
                if (Code[i] == '+')
                {
                    Accumulator++;
                }
                if (Code[i] == '-')
                {
                    Accumulator--;
                }
                if (Code[i] == '=')
                {
                    NextCharacter = (char)Accumulator;
                    OutputContents.Append(NextCharacter);
                }
                if (Code[i] == '|')
                {
                    Output = OutputContents.ToString();
                    break;
                }
                if (Code[i] == ' ')
                {
                    UserInput = Console.ReadLine();
                }
                if (Code[i] == '*')
                {
                    OutputContents.Append(UserInput);
                }
                if (Code[i] == ':')
                {
                    OutputContents.Clear();
                    OutputContents.Append(Code);
                    Output = OutputContents.ToString();
                    break;
                }
                if (Code[i] == '.')
                {
                    Console.Write(OutputContents.ToString());
                }
                if (Code[i] == ',')
                {
                    OutputContents.Clear();
                }
                if (Code[i] == '~')
                {
                    Accumulator = 0;
                }
                i++;
                CodePosition = i;
            }
        }
    }
}