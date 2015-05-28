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

        public IList<IList<int>> Permute(int[] nums)
        {
            var result = new List<IList<int>>();
            this.Permute(nums, 0, result);
            return result;
        }

        public void Permute(int[] nums, int idx, IList<IList<int>> result)
        {
            if (nums == null || nums.Length == 0 || idx > nums.Length) { return; }

            if (idx == nums.Length)
            {
                result.Add(nums.ToList());
                return;
            }

            for (int i = idx; i < nums.Length; i++)
            {
                if (i != idx && nums[i] == nums[idx]) { continue; }
                this.swap(nums, i, idx);
                this.Permute(nums, idx + 1, result);
                this.swap(nums, i, idx);
            }

            return;
        }

        private void swap(int[] nums, int i, int j)
        {
            var tmp = nums[i];
            nums[i] = nums[j];
            nums[j] = tmp;
        }

        public int UniquePaths(int m, int n) {
            return this.UniquePaths(0, 0, m, n);
        }

        private int UniquePaths(int i, int j, int m, int n) {
            if (i > m || i < 0 || j > n || j < 0) { return 0; }
            if (m == i)
            {
                return 1;
            }
            else if (n == j) {
                return 1;
            }

            return this.UniquePaths(i, j + 1, m, n) + this.UniquePaths(i + 1, j, m, n);
        }

        public int MinPathSum(int[,] grid)
        {
            for (int i = 0; i < grid.GetLength(0); i++) {
                for (int j = 0; j < grid.GetLength(1); j++) {
                    int sum1 = Int32.MaxValue;
                    int sum2 = Int32.MaxValue;
                    int sum = 0;
                    if (IsValidIdx(grid, i - 1, j)) {
                        sum1 = grid[i - 1, j];
                    }

                    if (IsValidIdx(grid, i, j - 1)) {
                        sum2 = grid[i, j - 1];
                    }

                    if (sum1 != Int32.MaxValue || sum2 != Int32.MaxValue) {
                        sum = Math.Min(sum1, sum2);
                    }

                    grid[i, j] = grid[i, j] + sum;
                }
            }

            return grid[grid.GetLength(0) - 1, grid.GetLength(1) - 1];
        }

        private bool IsValidIdx(int[,] grid, int i, int j)
        {
            return !(i < 0 || i >= grid.GetLength(0) || j < 0 || j >= grid.GetLength(1));
        }

        private int MinPathSum(int[,] grid, int i, int j, int current) {
            if (i >= grid.GetLength(0) || i < 0 || j >= grid.GetLength(1) || j < 0) { return Int32.MaxValue; }
            if (i == grid.GetLength(0) - 1 && j == grid.GetLength(1) - 1) {
                return current;
            }

            var sum1 = this.MinPathSum(grid, i + 1, j, current + grid[i, j]);
            var sum2 = this.MinPathSum(grid, i, j + 1, current + grid[i, j]);

            return (sum1 < sum2) ? sum1 : sum2;
        }

        public IList<IList<int>> Combine(int n, int k)
        {
            var result = new List<IList<int>>();
            var input = new int[n];
            for (int i = 1; i < n + 1; i++)
            {
                input[i - 1] = i;
            }
            this.Combine(input, 0, k, result);
            return result;
        }

        private void Combine(int[] n, int idx, int k, IList<IList<int>> result)
        {
            if (n == null || idx < 0 || idx > n.Length - 1 || result == null) { return; }
            if (idx == k)
            {
                var l = new List<int>();
                for (int i = 0; i < k; i++)
                {
                    l.Add(n[i]);
                }
                result.Add(l);
                return;
            }

            for (int i = idx; i < n.Length; i++)
            {
                this.swap(n, i, idx);
                this.Combine(n, idx + 1, k, result);
                this.swap(n, i, idx);
            }
        }

        public bool Exist(char[,] board, string word)
        {
            bool flag = false;
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                { 
                    if (this.Exist(board, word, i, j, "", new HashSet<Tuple<int, int>>())){
                        return true;
                    } 
                }
            return false;
        }

        private bool Exist(char[,] board, string word, int i, int j, string current, HashSet<Tuple<int, int>> visited) {
            if (!IsValidIdx(board, i, j) || current == null || word == null || visited == null || visited.Contains(new Tuple<int, int>(i, j))) {
                return false;
            }

            visited.Add(new Tuple<int, int>(i, j));
            current = current + board[i, j];
            if (current == word) { return true; }
            if (current.Length == word.Length && current != word) { return false; }
            
            return Exist(board, word, i - 1, j - 1, current, visited) 
            || Exist(board, word, i - 1, j, current, visited)
            || Exist(board, word, i - 1, j + 1, current, visited)
            || Exist(board, word, i, j - 1, current, visited)
            || Exist(board, word, i, j + 1, current, visited)
            || Exist(board, word, i + 1, j - 1, current, visited)
            || Exist(board, word, i + 1, j, current, visited)
            || Exist(board, word, i + 1, j + 1, current, visited);
        }

        private bool IsValidIdx(char[,] board, int i, int j)
        {
            return !(board == null || i < 0 || i >= board.GetLength(0) || j < 0 || j >= board.GetLength(1));
        }

        public float ToFloat(char[] input) {
            if (input == null || input.Length == 0) { return 0; }
            float result = 0;
            int decimalPosition = this.findDecimal(input);
            int pow = (decimalPosition != -1) ? decimalPosition - (input.Length - 1) : 0;
            for (int i = input.Length - 1; i >= 0; i--) {
                if (i != decimalPosition)
                {
                    result = result + ((int)Char.GetNumericValue(input[i])) * (float)Math.Pow(10, pow);
                    pow++;
                }
            }

            return result;
        }

        private int findDecimal(char[] input) {
            if (input == null) return -1;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '.') { return i; }
            }

            return -1;
        }

        private void PopulateInorderSuccesor(InOrderTreeNode node)
        {
            if (node == null) return;
            node.Next = this.successor(node);
            PopulateInorderSuccesor(node.Left);
            PopulateInorderSuccesor(node.Right);
        }

        private InOrderTreeNode successor(InOrderTreeNode node)
        {
            if (node == null) { return null; }
            if (node.Right != null)
            {
                return this.min(node.Right);
            }
            else 
            {
                InOrderTreeNode n1 = node.Parent;
                InOrderTreeNode n2 = node;
                InOrderTreeNode result = null;
                while (n1 != null)
                {
                    if (n1.Left == n2)
                    {
                        result = n1;
                        break;
                    }
                    else 
                    {
                        n2 = n1;
                        n1 = n1.Parent;
                    }
                }
                return result;
            }
        }

        private InOrderTreeNode min(InOrderTreeNode node)
        {
            if (node.Left == null) { return node; }
            return min(node.Left);
        }

        public int MaxClusterSize(int[,] input)
        {
            var visited = new HashSet<Tuple<int, int>>();
            var maxClusterSize = 0;
            for (int i = 0; i < input.Length; i++) {
                for (int j = 0; j < input.Length; j++) {
                    if (!visited.Contains(new Tuple<int, int>(i, j))) {
                        var clusterSize = this.MaxClusterSize(input, i, j, visited, 0);
                        if (clusterSize > maxClusterSize) {
                            maxClusterSize = clusterSize;
                        }
                    }
                }
            }

            return maxClusterSize;
        }

        private int MaxClusterSize(int[,] input, int i, int j, HashSet<Tuple<int, int>> visited, int size)
        {
            if (input == null || visited == null || !IsValidIdx(input, i, j)) {
                return 0;
            }

            if (visited.Contains(new Tuple<int, int>(i, j))) {
                return 0;
            }

            visited.Add(new Tuple<int, int>(i, j));
            if (input[i, j] == 0) {
                return 0;
            }

            size++;

            size = Math.Max(this.MaxClusterSize(input, i - 1, j, visited, size), size);
            size = Math.Max(this.MaxClusterSize(input, i + 1, j, visited, size), size);
            size = Math.Max(this.MaxClusterSize(input, i, j - 1, visited, size), size);
            size = Math.Max(this.MaxClusterSize(input, i, j + 1, visited, size), size);

            return size;
        }

        public string BinarySum(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) && string.IsNullOrEmpty(s2)) { return null; }
            else if (string.IsNullOrEmpty(s1)) { return s2; }
            else if (string.IsNullOrEmpty(s2)) { return s1; }

            int i1 = s1.Length - 1;
            int i2 = s2.Length - 1;
            int carry = 0;
            StringBuilder sb = new StringBuilder();

            while (i1 >= 0 && i2 >= 0) {
                int num1 = (int)Char.GetNumericValue(s1[i1]);
                int num2 = (int)Char.GetNumericValue(s2[i2]);
                var t = this.BinarySum(num1, num2, carry);
                carry = t.Item2;
                sb.Append(t.Item1);
                i1--;
                i2--;
            }

            while (i1 >= 0)
            {
                int num1 = (int)Char.GetNumericValue(s1[i1]);
                var t = this.BinarySum(num1, 0, carry);
                carry = t.Item2;
                sb.Append(t.Item1);
                i1--;
            }

            while (i2 >= 0)
            {
                int num1 = (int)Char.GetNumericValue(s1[i1]);
                var t = this.BinarySum(num1, 0, carry);
                carry = t.Item2;
                sb.Append(t.Item1);
                i2--;
            }

            if (carry > 0) {
                sb.Append(carry);
            }

            var s = sb.ToString().ToCharArray();
            Array.Reverse(s);
            return new string(s);
        }

        private Tuple<int, int> BinarySum(int num1, int num2, int num3)
        {
            if (num1 + num2 + num3 == 3) { return new Tuple<int, int>(1, 1); }
            else if (num1 + num2 + num3 == 2) { return new Tuple<int, int>(0, 1); }
            else if (num1 + num2 + num3 == 1) { return new Tuple<int, int>(1, 0); }
            else { return new Tuple<int, int>(0, 0); }
        }

        public Tuple<int, int> LongestPositiveSequence(int[] input)
        {
            if (input == null || input.Length == 0) return null;

            int maxCount = 0, maxStartIdx = -1, startIdx = -1, currentCount = 0;
            for (int i = 0; i < input.Length; i++) {
                if (input[i] > 0)
                {
                    if (startIdx == -1) { startIdx = i; }
                    currentCount++;
                    if (currentCount > maxCount) { maxCount = currentCount; maxStartIdx = startIdx; }
                }
                else
                {
                    startIdx = -1;
                    currentCount = 0;
                }
            }

            return new Tuple<int, int>(maxCount, maxStartIdx);
        }

        
    }

    class InOrderTreeNode
    {
        public int Data { get; set; }
        public InOrderTreeNode Parent { get; set; }
        public InOrderTreeNode Left { get; set; }
        public InOrderTreeNode Right { get; set; }
        public InOrderTreeNode Next { get; set; }
    }
}
