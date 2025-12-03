using System;

namespace AdventOfCode_2025_day02;

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
		string[] list = input[0].Split(",");
		long sum = 0;

		foreach (string idRange in list) {
			string[] range = idRange.Split("-");
			long firstId = long.Parse(range[0]);
			long lastId = long.Parse(range[1]);
			long i = (long)Math.Pow(10, Math.Floor((double)(range[0].Length/2))-1);
			long number = 0;

			while (true) {
				number = (long)((i * Math.Pow(10, Math.Floor(Math.Log10(i) + 1))) + i);

				if (number >= firstId && number <= lastId) {
					sum += number;
				}

				if (number > lastId) break;

				i++;
			}

		}

		return sum;
	}
}
