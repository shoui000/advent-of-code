using System;
using System.Collections;
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
		long spSolution = s.SecondPuzzle();

		Console.WriteLine($"FirstPuzzle: The answer is {fpSolution}");
		Console.WriteLine($"SecondPuzzle: The answer is {spSolution}");
	}
}

class Ranges : IEnumerable<long[]> {
	private List<long[]> ranges;

	public Ranges() {
		ranges = new List<long[]>();
	}

	public void addRange(long[] rg) {
		bool overlapped = false;

		foreach (long[] r in ranges) {
			if (rg[1] < r[0] || rg[0] > r[1]) {
				continue;
			}

			overlapped = true;

			if (rg[0] >= r[0] && rg [1] <= r[1]) {
				break;
			}

			if (rg[1] >= r[0] && rg[1] <= r[1]) {
				this.addRange(new long[] {rg[0], r[0]-1});
				break;
			} else if (rg[0] <= r[1] && rg[0] >= r[0]) {
				this.addRange(new long[] {r[1]+1, rg[1]});
				break;
			} else if (rg[0] < r[0] && rg[1] > r[1]) {
				this.addRange(new long[] {rg[0], r[0]-1});
				this.addRange(new long[] {r[1]+1, rg[1]});
				break;
			}
		}

		if (!overlapped) {
			ranges.Add(rg);
		}
	}

	public IEnumerator<long[]> GetEnumerator() {
		return ((IEnumerable<long[]>)ranges).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return GetEnumerator();
	}

	public override string ToString() {
		return string.Join(", ", ranges.Select(r => $"[{r[0]}, {r[1]}]"));
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

	public long SecondPuzzle() {
		long sum = 0;
		Ranges rgs = new Ranges();

		foreach (string line in input) {
			if (line == "") {
				break;
			}

			string[] a = line.Split("-");
			long[] rg = new long[] { long.Parse(a[0]), long.Parse(a[1]) };
			rgs.addRange(rg);

		}

		foreach (long[] r in rgs) {
			sum += (r[1] - r[0]) + 1;
		}

		return sum;
	}
}

