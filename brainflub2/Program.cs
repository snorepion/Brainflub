using System;
using System.IO;

class Brainflub2Reader
{
    // the accumulator (single int variable that is used to interface with pouts directly)
    static int acc = 0;
    // indicates the number of the active pout (only one can be active at a time)
    static int active_pout = 0;
    // the current position, for iterating through code
    static int char_num = 0;
    // lets the code runner know whether to interpret ambiguous operators (= + * >) as conditional or regular, this marks the interior of braces {X}
    static bool in_conditional = false;
    // this tells the program whether a statement is dependent on a previously-satisfied conditional statement; cannot be used after a statement of the form {*##} or {+##}
    static bool conditional_response = false;
    // tells the program what the result of the last conditional was
    static bool conditional_result = false;
    // tells the program whether to skip code for being in a comment
    static bool comment_mode = false;
    // the type of compare/combine operation (possible values are . for nothing and + = * > < ] [ ! $)
    static char conditional_mode = '.';
    // pout values that will be compared/combined in a {...}.+> expression (default to invalid value 10)
    static int conditional_1 = 10;
    static int conditional_2 = 10;
    // pouts are 10 static string variables that can be reassigned and used as output
    static string pout0 = "";
    static string pout1 = "";
    static string pout2 = "";
    static string pout3 = "";
    static string pout4 = "";
    static string pout5 = "";
    static string pout6 = "";
    static string pout7 = "";
    static string pout8 = "";
    static string pout9 = "";
    static string uinput = "";
    // contents of each file argument
    static string conts;
    static void AddPout(string value)
    {
        switch (active_pout)
        {
            case 0:
                {
                    pout0 += value;
                    break;
                }
            case 1:
                {
                    pout1 += value;
                    break;
                }
            case 2:
                {
                    pout2 += value;
                    break;
                }
            case 3:
                {
                    pout3 += value;
                    break;
                }
            case 4:
                {
                    pout4 += value;
                    break;
                }
            case 5:
                {
                    pout5 += value;
                    break;
                }
            case 6:
                {
                    pout6 += value;
                    break;
                }
            case 7:
                {
                    pout7 += value;
                    break;
                }
            case 8:
                {
                    pout8 += value;
                    break;
                }
            case 9:
                {
                    pout9 += value;
                    break;
                }
            default:
                return;
        }
    }
    static void SetPout(string value)
    {
        switch (active_pout)
        {
            case 0:
                {
                    pout0 = value;
                    break;
                }
            case 1:
                {
                    pout1 = value;
                    break;
                }
            case 2:
                {
                    pout2 = value;
                    break;
                }
            case 3:
                {
                    pout3 = value;
                    break;
                }
            case 4:
                {
                    pout4 = value;
                    break;
                }
            case 5:
                {
                    pout5 = value;
                    break;
                }
            case 6:
                {
                    pout6 = value;
                    break;
                }
            case 7:
                {
                    pout7 = value;
                    break;
                }
            case 8:
                {
                    pout8 = value;
                    break;
                }
            case 9:
                {
                    pout9 = value;
                    break;
                }
            default:
                return;
        }
    }
    static string GetPout()
    {
        switch (active_pout)
        {
            case 0:
                return pout0;
            case 1:
                return pout1;
            case 2:
                return pout2;
            case 3:
                return pout3;
            case 4:
                return pout4;
            case 5:
                return pout5;
            case 6:
                return pout6;
            case 7:
                return pout7;
            case 8:
                return pout8;
            case 9:
                return pout9;
            default:
                return "";
        }
    }
    static void ClearPouts()
    {
        pout0 = "";
        pout1 = "";
        pout2 = "";
        pout3 = "";
        pout4 = "";
        pout5 = "";
        pout6 = "";
        pout7 = "";
        pout8 = "";
        pout9 = "";
    }
    private static void Main(string[] args)
    {
        foreach (string e in args)
        {
            conts = File.ReadAllText(e);
            foreach (char c in conts)
            {
                if (conditional_response == true && c != '>' && conditional_result == false || comment_mode == true && c != '%')
                {
                    // do nothing
                }
                else
                {
                    switch (c)
                    {
                        case '+':
                            {
                                if (in_conditional)
                                {
                                    conditional_mode = '+';
                                }
                                else
                                {
                                    acc++;
                                }
                                break;
                            }
                        case '-':
                            {
                                acc--;
                                break;
                            }
                        case '=':
                            {
                                if (in_conditional == false)
                                {
                                    AddPout(((char)acc).ToString());
                                }
                                else
                                {
                                    conditional_mode = '=';
                                }
                                break;
                            }
                        case '|':
                            {
                                AddPout(((char)acc).ToString());
                                Console.Write(GetPout());
                                SetPout("");
                                goto end_of_file;
                            }
                        case ':':
                            {
                                Console.Write(conts);
                                SetPout("");
                                goto end_of_file;
                            }
                        case '.':
                            {
                                Console.Write(GetPout());
                                break;
                            }
                        case ',':
                            SetPout("");
                            break;
                        // reset accumulator
                        case '~':
                            acc = 0;
                            break;
                        // take user input (in brainflub, this was a space; all whitespace is now ignored!!)
                        // this is the sole case where brainflub2 is not backwards-compatible; simply use find and replace on old fl scripts to change spaces to _
                        case '_':
                            {
                                uinput = Console.ReadLine();
                                break;
                            }
                        case '%':
                            {
                                comment_mode = !comment_mode;
                                break;
                            }
                        case '*':
                            {
                                if (in_conditional)
                                {
                                    conditional_mode = '*';
                                }
                                else
                                {
                                    AddPout(uinput);
                                }
                                break;
                            }
                        case '#':
                            acc += 10;
                            break;
                        case '/':
                            acc -= 10;
                            break;
                        case '{':
                            // should be immediately followed by one of: > < [ ] = ! * + $ and pout numbers
                            in_conditional = true;
                            break;
                        case '}':
                            in_conditional = false;
                            int orig_active = active_pout;
                            active_pout = conditional_1;
                            string conditional_value1 = GetPout();
                            // ***
                            active_pout = conditional_2;
                            string conditional_value2 = GetPout();
                            switch (conditional_mode)
                            {
                                case '=':

                                    conditional_response = true;
                                    conditional_result = (conditional_value1 == conditional_value2);
                                    break;
                                case '<':
                                    conditional_response = true;
                                    conditional_result = (Int32.Parse(conditional_value1) < Int32.Parse(conditional_value2));
                                    break;
                                case '>':
                                    conditional_response = true;
                                    conditional_result = (Int32.Parse(conditional_value1) > Int32.Parse(conditional_value2));
                                    break;
                                case '[':
                                    conditional_response = true;
                                    conditional_result = (Int32.Parse(conditional_value1) <= Int32.Parse(conditional_value2));
                                    break;
                                case ']':
                                    conditional_response = true;
                                    conditional_result = (Int32.Parse(conditional_value1) >= Int32.Parse(conditional_value2));
                                    break;
                                case '!':
                                    conditional_response = true;
                                    conditional_result = (conditional_value1 != conditional_value2);
                                    break;
                                case '*':
                                    active_pout = conditional_1;
                                    SetPout(conditional_value2);
                                    break;
                                case '+':
                                    active_pout = conditional_1;
                                    AddPout(conditional_value2);
                                    break;
                                case '$':
                                    // don't have to explicitly set to the second conditional pout b/c it's already loaded as active (see line commented *** above)
                                    SetPout(File.ReadAllText(conditional_value1));
                                    break;
                                default:
                                    break;
                            }
                            conditional_1 = 10;
                            conditional_2 = 10;
                            active_pout = orig_active;
                            break;
                        // Conditional handlers for unique conditional/reallocation-related operators (exc = + *)
                        // greater than/fi
                        case '>':
                            if (in_conditional)
                            {
                                conditional_mode = '>';
                            }
                            else
                            {
                                conditional_response = false;
                            }
                            break;
                        // less than
                        case '<':
                            if (in_conditional) conditional_mode = '<';
                            break;
                        // greater than or equal to
                        case ']':
                            if (in_conditional) conditional_mode = ']';
                            break;
                        // less than or equal to
                        case '[':
                            if (in_conditional) conditional_mode = '[';
                            break;
                        // not equal to
                        case '!':
                            if (in_conditional) conditional_mode = '!';
                            break;
                        // load filename from first argument to second argument
                        case '$':
                            if (in_conditional) conditional_mode = '$';
                            break;
                        // end of cond/realloc ops
                        // restart program
                        case '&':
                            acc = 0;
                            ClearPouts();
                            Main(new string[] { e });
                            break;
                        // execute filename in active pout
                        case '^':
                            Main(new string[] { GetPout() });
                            break;
                        // handlers for pouts
                        case '0':
                            {
                                if (in_conditional)
                                {
                                    // set conditional 1 if unset
                                    if (conditional_1 == 10)
                                    {
                                        conditional_1 = 0;
                                    }
                                    // if conditional 1 has already been set (assumed so, since it simply cannot be set to 10), set conditional 2
                                    else
                                    {
                                        if (conditional_2 == 10)
                                        {
                                            conditional_2 = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    active_pout = 0;
                                }
                                break;
                            }
                        case '1':
                            {
                                if (in_conditional)
                                {
                                    if (conditional_1 == 10)
                                    {
                                        conditional_1 = 1;
                                    }
                                    else
                                    {
                                        if (conditional_2 == 10)
                                        {
                                            conditional_2 = 1;
                                        }
                                    }
                                }
                                else
                                {
                                    active_pout = 1;
                                }
                                break;
                            }
                        case '2':
                            {
                                if (in_conditional)
                                {
                                    if (conditional_1 == 10)
                                    {
                                        conditional_1 = 2;
                                    }
                                    else
                                    {
                                        if (conditional_2 == 10)
                                        {
                                            conditional_2 = 2;
                                        }
                                    }
                                }
                                else
                                {
                                    active_pout = 2;
                                }
                                break;
                            }
                        case '3':
                            {
                                if (in_conditional)
                                {
                                    if (conditional_1 == 10)
                                    {
                                        conditional_1 = 3;
                                    }
                                    else
                                    {
                                        if (conditional_2 == 10)
                                        {
                                            conditional_2 = 3;
                                        }
                                    }
                                }
                                else
                                {
                                    active_pout = 3;
                                }
                                break;
                            }
                        case '4':
                            {
                                if (in_conditional)
                                {
                                    if (conditional_1 == 10)
                                    {
                                        conditional_1 = 4;
                                    }
                                    else
                                    {
                                        if (conditional_2 == 10)
                                        {
                                            conditional_2 = 4;
                                        }
                                    }
                                }
                                else
                                {
                                    active_pout = 4;
                                }
                                break;
                            }
                        case '5':
                            {
                                if (in_conditional)
                                {
                                    if (conditional_1 == 10)
                                    {
                                        conditional_1 = 5;
                                    }
                                    else
                                    {
                                        if (conditional_2 == 10)
                                        {
                                            conditional_2 = 5;
                                        }
                                    }
                                }
                                else
                                {
                                    active_pout = 5;
                                }
                                break;
                            }
                        case '6':
                            {
                                if (in_conditional)
                                {
                                    if (conditional_1 == 10)
                                    {
                                        conditional_1 = 6;
                                    }
                                    else
                                    {
                                        if (conditional_2 == 10)
                                        {
                                            conditional_2 = 6;
                                        }
                                    }
                                }
                                else
                                {
                                    active_pout = 6;
                                }
                                break;
                            }
                        case '7':
                            {
                                if (in_conditional)
                                {
                                    if (conditional_1 == 10)
                                    {
                                        conditional_1 = 7;
                                    }
                                    else
                                    {
                                        if (conditional_2 == 10)
                                        {
                                            conditional_2 = 7;
                                        }
                                    }
                                }
                                else
                                {
                                    active_pout = 7;
                                }
                                break;
                            }
                        case '8':
                            {
                                if (in_conditional)
                                {
                                    if (conditional_1 == 10)
                                    {
                                        conditional_1 = 8;
                                    }
                                    else
                                    {
                                        if (conditional_2 == 10)
                                        {
                                            conditional_2 = 8;
                                        }
                                    }
                                }
                                else
                                {
                                    active_pout = 8;
                                }
                                break;
                            }
                        case '9':
                            {
                                if (in_conditional)
                                {
                                    if (conditional_1 == 10)
                                    {
                                        conditional_1 = 9;
                                    }
                                    else
                                    {
                                        if (conditional_2 == 10)
                                        {
                                            conditional_2 = 9;
                                        }
                                    }
                                }
                                else
                                {
                                    active_pout = 9;
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                char_num++;
            }
        end_of_file:;
            ClearPouts();
            uinput = "";
            char_num = 0;
        }
    }
}