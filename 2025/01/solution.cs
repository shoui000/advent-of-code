using System;

namespace AdventOfCode_2025_day01;

class Program {

	static void Main(string[] args) {
		int option = 0;

		if (args.Length > 0){
			option = int.Parse(args[0]);
		}

		Solution s = new Solution(option);
		int fpSolution = s.FirstPuzzle();
		int spSolution = s.SecondPuzzle();

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
		int pos = 50;
		int sum = 0;

		foreach (string line in input) {
			int delta = 0;

			delta = int.Parse(line.Substring(1)) % 100;

			if (line[0] == 'L') delta *= -1;

			pos += delta;
			
			if (pos < 0) pos += 100;
			if (pos > 99) pos %= 100;

			if (pos == 0) sum++;
		}

		return sum;
	}

	public int SecondPuzzle() {
		int pos = 50;
		int sum = 0;

		foreach (string line in input) {
			int move = int.Parse(line.Substring(1));

			if (move >= 100) {
				sum += move / 100;
				move %= 100;
			}
			if (line[0] == 'L' && move >= pos && pos != 0) sum++;
			if (line[0] == 'R' && move >= 100 - pos) sum++;


			if (line[0] == 'L') move *= -1;

			pos += move;
			
			if (pos < 0) pos += 100;
			if (pos > 99) pos %= 100;
		}

		return sum;

	}
}
