using System;


namespace AdventOfCode_2025_day03;

class Program {
	static void Main(string[] args) {
		int option = 0;

		if (args.Length > 0){
			option = int.Parse(args[0]);
		}

		Solution s = new Solution(option);
		long fpSolution = s.FirstPuzzle();

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

	public long FirstPuzzle() {
		long sum = 0;

		foreach (string line in input) {
			int max = line[0] - '0';
			int maxPos = 0;
			int secondMax;
			int fullNumber = 0;

			for (int i = 0; i < line.Length; i++) {
				if (line[i] - '0' > max) {
					max = line[i] - '0';
					maxPos = i;
				}
			}

			if (maxPos < line.Length-1) {
				secondMax = line[maxPos+1] - '0';

				for (int i = maxPos+1; i < line.Length; i++) {
					if (line[i] - '0' > secondMax) {
						secondMax = line[i] - '0';
					}
				}

				fullNumber = int.Parse($"{max}{secondMax}");
			} else if (maxPos == line.Length-1) {
				secondMax = line[0] - '0';

				for (int i = 0; i < line.Length-1; i++) {
					if (line[i] - '0' > secondMax) {
						secondMax = line[i] - '0';
					}
				}

				fullNumber = int.Parse($"{secondMax}{max}");
			}


			sum += fullNumber;

		}

		return sum;
	}
}
