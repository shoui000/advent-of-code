using System;
using System.Collections.Generic;

namespace AdventOfCode_2025_day01;

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
		List<long[]> ranges = new List<long[]>();

		bool ids = false;
		foreach (string line in input) {

			if (line == "") {
				ids = true;
				continue;
			}

			if (!ids) {
				string[] a = line.Split("-");
				ranges.Add(new long[] { long.Parse(a[0]), long.Parse(a[1]) });
			}

			if (ids) {
				long numero = long.Parse(line);

				foreach (long[] r in ranges) {
					if (numero >= r[0] && numero <= r[1]) {
						sum++;
						break;
					}
				}
			}

		}

		return sum;
	}
}

