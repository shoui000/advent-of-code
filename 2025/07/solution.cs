using System;
using System.Text;

namespace AdventOfCode_2025_day07;

class Program {
	static void Main(string[] args) {
		int option = 0;

		if (args.Length > 0){
			option = int.Parse(args[0]);
		}

		Solution s = new Solution(option);
		int fpSolution = s.FirstPuzzle();
		long spSolution = s.SecondPuzzle();

		Console.WriteLine($"FirstPuzzle: The answer is {fpSolution}");
		Console.WriteLine($"SecondPuzzle: The answer is {spSolution}");
	}
}

class Solution {
	private string[] input;

	public Solution(int op) {
		switch (op) {
			case 0: 
				input = File.ReadAllLines("./input_test.txt");
				break;
			case 1:
				input = File.ReadAllLines("./input.txt");
				break;
			default:
				input = [];
				break;
		}
	}
	
	public int FirstPuzzle() {
		int sum = 0;
		int rows = input.Length, cols = input[0].Length;
		char[,] matrix = new char[rows, cols];

		for (int i = 0; i < rows; i++)
			for (int j = 0; j < cols; j++)
				matrix[i, j] = input[i][j];

		if (matrix[1, input[0].IndexOf('S')] != '^') 
			matrix[1, input[0].IndexOf('S')] = '|';
		else {
			if (input[0].IndexOf('S') > 0)
				matrix[1, input[0].IndexOf('S')-1] = '|';
			
			if (input[0].IndexOf('S') < input[0].Length-1)
				matrix[1, input[0].IndexOf('S')+1] = '|';
		}

		for (int i = 1; i < rows-1; i++) {
			for (int j = 0; j < cols; j++) {

				if (matrix[i, j] != '|')
					continue;

				if (matrix[i+1, j] == '|')
					continue;

				if (matrix[i+1, j] == '.') {
					matrix[i+1, j] = '|';
					continue;
				}

				if (j > 0) 
					matrix[i+1, j-1] = '|';

				if (j < cols-1)
					matrix[i+1, j+1] = '|';

				sum++;


			}
		}

		return sum;
	}

	public long SecondPuzzle() {
		QuantumManifold qm = new QuantumManifold(input);

		return qm.Compute();
	}
}

class QuantumManifold {
	private char[,] matrix;
	private int rows, cols, startX, startY;
	private long?[,] memo;

	public QuantumManifold(string[] input) {
		rows = input.Length;
		cols = input[0].Length;
		matrix = new char[rows, cols];
		memo = new long?[rows, cols];

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < cols; j++) {
				matrix[i, j] = input[i][j];
			}
		}

		startX = 1;
		startY = input[0].IndexOf('S');
	}

	public long Compute() {
		return _Step(startY, startX);
	}

	public string ToString(int x, int y) {
		StringBuilder sb = new StringBuilder();

		for (int i = 0; i < rows; i++) {
			for (int j = 0; j < cols; j++) {
				if (i == y && j == x) {
					sb.Append('*');
				} else {
					sb.Append(matrix[i, j]);
				}
			}

			sb.Append('\n');
		}

		return sb.ToString();
	}

	private long _Step(int x, int y) {
		if (y == rows-1)
			return 1;

		if (memo[y, x].HasValue)
			return memo[y, x].GetValueOrDefault();

		long total = 0;
		List<long> branches = new List<long>();

		if (matrix[y+1, x] == '.')
			branches.Add(_Step(x, y+1));

		if (matrix[y+1, x] == '^') {
			if (x > 0)
				branches.Add(_Step(x-1, y+1));

			if (x < cols-1)
				branches.Add(_Step(x+1, y+1));
		}

		if (branches.Count > 0)
			total += branches[0];

		for (int i = 1; i < branches.Count; i++)
			total += branches[i];
		
		memo[y, x] = total;
		return total;

	}

}
