using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ParseArrayJsToCSharp
{
    class Program
    {
        static void Main( string[ ] args )
        {
            string expression = "a => [5255 ,526865.5, 541].Contains( a.numserie ) && [\"A157\", \"A451\"].Contains( a.prefix )";

            Console.WriteLine( expression );

            Regex r1 = new Regex( @"\[(.*?)\]" );

            MatchCollection matches = r1.Matches( expression );

            foreach ( Match match in matches )
            {
                JArray jArray = ( JArray ) JsonConvert.DeserializeObject( match.Value );

                switch ( jArray.First.Type )
                {
                    case JTokenType.Integer:
                        expression = expression.Replace( 
                            match.Value, 
                            string.Join( "", "(new int[] {", match.Value.Substring( 1, match.Value.Length - 2 ), "})" ) );
                        break;
                    case JTokenType.Float:
                        expression = expression.Replace(
                            match.Value,
                            string.Join( "", "(new double[] {", match.Value.Substring( 1, match.Value.Length - 2 ), "})" ) );
                        break;
                    case JTokenType.String:
                        expression = expression.Replace( 
                            match.Value,
                            string.Join( "", "(new string[] {", match.Value.Substring( 1, match.Value.Length - 2 ), "})" ) );
                        break;
                    case JTokenType.Boolean:
                        expression = expression.Replace(
                            match.Value,
                            string.Join( "", "(new bool[] {", match.Value.Substring( 1, match.Value.Length - 2 ), "})" ) );
                        break;
                    case JTokenType.Bytes:
                        expression = expression.Replace(
                            match.Value,
                            string.Join( "", "(new byte[] {", match.Value.Substring( 1, match.Value.Length - 2 ), "})" ) );
                        break;
                    default:
                        throw new Exception( "Type array not found" );
                }
            }

            Console.WriteLine( expression );
        }
    }
}
