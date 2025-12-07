using System;

namespace AdventOfCode_2025_day07;

class Program {
	static void Main(string[] args) {
		int option = 0;

		if (args.Length > 0){
			option = int.Parse(args[0]);
		}

		Solution s = new Solution(option);
		int fpSolution = s.FirstPuzzle();
		// int spSolution = s.SecondPuzzle();

		Console.WriteLine($"FirstPuzzle: The answer is {fpSolution}");
		// Console.WriteLine($"SecondPuzzle: The answer is {spSolution}");
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
}
