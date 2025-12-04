using System;

namespace AdventOfCode_2025_day04;

class Program {

	static void Main(string[] args) {
		int option = 0;

		if (args.Length > 0){
			option = int.Parse(args[0]);
		}

		Solution s = new Solution(option);
		int fpSolution = s.FirstPuzzle();

		Console.WriteLine($"FirstPuzzle: The answer is {fpSolution}");

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
		int localSum = 0;

		for (int i = 0; i < input.Length; i++) {
			for (int j = 0; j < input[0].Length; j++) {
				if (input[i][j] == '.') continue;

				localSum = 0;

				if (i > 0 && j > 0 && input[i-1][j-1] == '@') localSum++;
				if (i > 0 && input[i-1][j] == '@') localSum++;
				if (i > 0 && j < input[0].Length - 1 && input[i-1][j+1] == '@') localSum++;

				if (j > 0 && input[i][j-1] == '@') localSum++;
				if (j < input[0].Length - 1 && input[i][j+1] == '@') localSum++;

				if (i < input.Length - 1 && j > 0 && input[i+1][j-1] == '@') localSum++;
				if (i < input.Length - 1 && input[i+1][j] == '@') localSum++;
				if (i < input.Length - 1 && j < input[0].Length - 1 && input[i+1][j+1] == '@') localSum++;

				if (localSum < 4) sum++;
			}
		}

		return sum;
	}

}
