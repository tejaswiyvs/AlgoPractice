using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class ArrayProblems
    {
        public bool MazeRunner(int[,] maze, int i, int j, ArrayList path)
        {
            if (maze == null || !isValidIdx(i, j, maze) || path == null || path.Contains(new Tuple<int, int>(i, j))) { return false; }
            var pathComponent = new Tuple<int, int>(i, j);
            path.Add(pathComponent);
            if (i == maze.GetLength(0) - 1 && j == maze.GetLength(1) - 1) { return true; }
            var result = (this.MazeRunner(maze, i - 1, j - 1, path)
                || this.MazeRunner(maze, i - 1, j, path)
                || this.MazeRunner(maze, i - 1, j + 1, path)
                || this.MazeRunner(maze, i, j - 1, path)
                || this.MazeRunner(maze, i, j + 1, path)
                || this.MazeRunner(maze, i + 1, j - 1, path)
                || this.MazeRunner(maze, i + 1, j, path)
                || this.MazeRunner(maze, i + 1, j + 1, path));
            if (!result) { path.Remove(pathComponent); }
            return result;
        }

        private bool isValidIdx(int i, int j, int[,] input)
        {
            return (i >= 0 && i < input.GetLength(0) && j >= 0 && j < input.GetLength(1) && input[i, j] != 1);
        }

        public string longestSubstringWithoutRepeatingCharacters(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string maxSubstring = "";
            StringBuilder current = new StringBuilder();
            HashSet<char> currentSet = new HashSet<char>();
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i; j < input.Length; j++)
                {
                    char ch = input[j];
                    if (currentSet.Contains(ch)) {
                        break;
                    }
                    currentSet.Add(ch);
                    current.Append(ch);
                }

                if (current.Length > maxSubstring.Length)
                {
                    maxSubstring = current.ToString();
                }
                current = new StringBuilder();
                currentSet = new HashSet<char>();
            }

            return maxSubstring;
        }

        public int medianOfTwoSortedArrays(int[] input1, int[] input2)
        {
            if (input1 == null || input2 == null)
            {
                return Int32.MinValue;
            }

            int[] holder = new int[input1.Length + input2.Length];
            int i = 0, j = 0;
            while (i < input1.Length && j < input2.Length)
            {
                if (input1[i] <= input2[j])
                {
                    holder[i + j] = input1[i];
                    i++;
                }
                else
                {
                    holder[i + j] = input2[j];
                    j++;
                }
            }

            while (i < input1.Length)
            {
                holder[i + j] = input1[i];
                i++;
            }

            while (j < input2.Length)
            {
                holder[i + j] = input2[j];
                j++;
            }

            var middleIdx = (int)Math.Floor(holder.Length / 2.0);
            var item = holder[middleIdx];
            return item;
        }

        public int longestPalindromicSubstring(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }

            int maxLengthPalindrome = 1;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (isPalindrome(input, i, j) && (j - i) > maxLengthPalindrome)
                    {
                        maxLengthPalindrome = (j - i);
                    }
                }
            }

            return maxLengthPalindrome;
        }

        private bool isPalindrome(string input, int i, int j)
        {
            if (string.IsNullOrEmpty(input) || j < i) return false;
            if (j - i <= 1)
            {
                return true;
            }
            for (int idx = i; idx < (j - i) / 2; idx++)
            {
                if (input[idx] != input[j - i - idx]) {
                    return false;
                }   
            }

            return true;
        }

        public int reverseInteger(int input)
        {
            bool negative = false;
            if (input < 0) { negative = true; input = Math.Abs(input); }

            int pow = 0;
            while (input > 0) {
                input /= 10;
                pow++;
            }

            int result = 0;
            while (input > 0) {
                var digit = input % 10;
                result = result + (int)(digit * Math.Pow(10, pow));
                pow--;
                input /= 10;
            }

            return negative ? -1 * result : result;
        }

        public int atoi(string input)
        {
            if (string.IsNullOrEmpty(input)) { return 0; }

            int result = 0;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                int digit = input[i] - '0';
                result = result + digit * (int)(Math.Pow(10, input.Length - 1 - i));
            }

            return result;
        }

        // Getting frustrated with the edge cases. TBD
        public int regexMatch(string input, string pattern, int inputIdx, int patternIdx) {
            throw new NotImplementedException();
        }

        public int MaxArea(int[] height)
        { 
            // I could only get the O(n^2) brute force approach to this. I don't understand the O(n) approach.
            if (height == null || height.Length <= 1) { return Int32.MinValue; }

            int maxArea = 0;
            for (int i = 0; i < height.Length; i++)
            {
                for (int j = i + 1; j < height.Length; j++)
                {
                    var area = (j - i) * Math.Min(height[i], height[j]);
                    if (area > maxArea) { maxArea = area; }
                }
            }

            return maxArea;
        }

        public string IntToRoman(int num)
        {
            int maxPow = this.maxPower(num);
            int pow = maxPow;
            string result = "";
            while (pow >= 0) {
                var digit = num / ((int)Math.Pow(10, pow));
                result = result + this.ConvertDigit(digit, pow);
                num = num - (digit * (int)Math.Pow(10, pow));
                pow--;
            }

            return result;
        }

        private string ConvertDigit(int digit, int pow)
        {
            var romanMapping = getRomanMapping();
            var result = "";
            
            if (digit == 0)
            {
                return "";
            }

            if (digit == 9)
            {
                return romanMapping[(int)Math.Pow(10, pow)] + romanMapping[(int)Math.Pow(10, pow + 1)];
            }

            if (digit == 4)
            {
                return romanMapping[(int)Math.Pow(10, pow)] + romanMapping[5 * (int)Math.Pow(10, pow)];
            }

            if (digit >= 5)
            { 
                result = result + romanMapping[5 * (int)Math.Pow(10, pow)];
                digit -= 5;
            }

            while (digit > 0)
            {
                result += romanMapping[(int)Math.Pow(10, pow)];
                digit--;
            }

            return result;
        }

        private Dictionary<int, string> getRomanMapping()
        {
            Dictionary<int, string> romanMapping = new Dictionary<int, string>();
            romanMapping[1] = "I";
            romanMapping[5] = "V";
            romanMapping[10] = "X";
            romanMapping[50] = "L";
            romanMapping[100] = "C";
            romanMapping[500] = "D";
            romanMapping[1000] = "M";

            return romanMapping;
        }

        private int maxPower(int num)
        {
            int pow = 0;
            while (num > 0)
            {
                num = num / 10;
                pow++;
            }
            return pow - 1;
        }

        public int RomanToInt(string s)
        {
            if (string.IsNullOrEmpty(s)) {
                return Int32.MinValue;
            }

            var mapping = RomanToIntMapping();
            int result = 0;
            for (int i = 0; i < s.Length; i++)
            {
                var currentVal = mapping[s[i]];
                var nextVal = 0;

                if (i < s.Length - 1)
                {
                    nextVal = mapping[s[i + 1]];
                }
                if (currentVal < nextVal)
                {
                    currentVal = nextVal - currentVal;
                    i++;
                }

                result = result + currentVal;
            }

            return result;
        }

        private Dictionary<char, int> RomanToIntMapping()
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            dict['M'] = 1000;
            dict['D'] = 500;
            dict['C'] = 100;
            dict['L'] = 50;
            dict['X'] = 10;
            dict['V'] = 5;
            dict['I'] = 1;
            return dict;
        }

        public string LongestCommonPrefix(string[] input)
        {
            if (input == null || input.Length == 0) return "";
            if (input.Length == 1) return input[0];

            var minLength = this.findMinLengthString(input);

            string prefix = "";
            for (int i = 0; i < minLength; i++)
            {
                char ch = '\0';
                for (int j = 0; j < input.Length; j++)
                {
                    if (ch == '\0')
                    {
                        ch = input[j][i];
                    }
                    else if (input[j][i] != ch)
                    {
                        return prefix;
                    }
                }
                prefix = prefix + ch;
            }

            return prefix;
        }

        private int findMinLengthString(string[] input)
        {
            if (input == null || input.Length == 0) { return -1; }

            var minLength = Int32.MaxValue;
            foreach (var s in input)
            {
                if (s.Length < minLength)
                {
                    minLength = s.Length;
                }
            }

            return minLength;
        }

        public IList<IList<int>> ThreeSum(int[] nums)
        {
            if (nums == null || nums.Length < 3) { return null; }

            HashSet<int> set = new HashSet<int>();
            foreach (var num in nums) {
                set.Add(num);
            }

            var result = new List<IList<int>>();
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if (set.Contains(-1 * (nums[i] + nums[j]))) 
                    {
                        var list = new List<int>();
                        list.Add(nums[i]);
                        list.Add(nums[j]);
                        list.Add(-1 * (nums[i] + nums[j]));
                        list.Sort();
                        result.Add(list);
                    }
                }
            }

            return result;
        }

        public IList<string> LetterCombinations(string digits)
        {
            return this.LetterCombinations(digits, 0, "", new List<string>());
        }

        private IList<string> LetterCombinations(string digits, int idx, string currentResult, IList<string> results)
        {
            if (string.IsNullOrEmpty(digits) || idx > digits.Length || results == null) { return new List<string>(); }

            if (idx == digits.Length)
            {
                results.Add(currentResult);
                return results;
            }

            var map = this.getMap();
            int num = (int)Char.GetNumericValue(digits[idx]);
            if (map[num] == "") {
                return this.LetterCombinations(digits, idx + 1, currentResult, results);
            }
            foreach (var ch in map[num])
            {
                this.LetterCombinations(digits, idx + 1, currentResult + ch, results);  
            }

            return results;
        }

        private Dictionary<int, string> getMap()
        {
            var dict = new Dictionary<int, string>();
            dict[1] = "";
            dict[2] = "abc";
            dict[3] = "def";
            dict[4] = "ghi";
            dict[5] = "jkl";
            dict[6] = "mno";
            dict[7] = "pqrs";
            dict[8] = "tuv";
            dict[9] = "wxyz";
            dict[0] = "";
            return dict;
        }

        public bool IsValid(string s)
        {
            if (string.IsNullOrEmpty(s)) { return true; }
            var validParens = this.GetValidParen();
            Stack<char> paren = new Stack<char>();

            foreach (var ch in s)
            {
                for (int i = 0; i < validParens.Count; i++) {
                    // A brace is being opened. Just push onto stack
                    if (ch == validParens[i].Item1)
                    {
                        paren.Push(ch);
                        break;
                    }
                    // We're closing a brace. Let's check if the closing paren matches the top of the item in the stack
                    else if (ch == validParens[i].Item2)
                    {
                        if (paren.Count == 0 || paren.Peek() != validParens[i].Item1) { return false; }
                        paren.Pop();
                        break;
                    }
                }
            }

            return (paren.Count == 0);
        }

        private List<Tuple<char, char>> GetValidParen()
        {
            var validParen = new List<Tuple<char, char>>();
            validParen.Add(new Tuple<char, char>('(', ')'));
            validParen.Add(new Tuple<char, char>('{', '}'));
            validParen.Add(new Tuple<char, char>('[', ']'));
            return validParen;
        }

        public IList<string> GenerateParenthesis(int n)
        {
            if (n == 0) { return null; }
            var result = new List<string>();
            this.GenerateParenthesis(n, n, "", result);
            return result;
        }

        private void GenerateParenthesis(int nOpen, int nClosed, string current, IList<string> result)
        {
            if (current == null || nOpen < 0 || nClosed <0 || result == null) { return; }
            if (nOpen == 0 && nClosed == 0) {
                result.Add(current);
                return;
            }

            if (nOpen > 0) {
                this.GenerateParenthesis(nOpen - 1, nClosed, current + "(", result);
            }
            if (nOpen < nClosed) {
                this.GenerateParenthesis(nOpen, nClosed - 1, current + ")", result);
            }
        }
    }
}
