using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Command;


static void printanything<T>(IEnumerable<T> xx,char limiter = ',')
{
	foreach (var x in xx)
	{
		Console.Write($"{x}{limiter} ");
	}
	Console.WriteLine();
}

static void print2d<T>(IEnumerable<IEnumerable<T>> vs,char limiter = ',')
{
	foreach (var item in vs)
	{
		printanything<T>(item,limiter);
	}
}

static void day1()
{
	StreamReader txt = new StreamReader(@"C:\Users\11349\source\repos\Advent\input1.txt");
	var txts = txt.ReadToEnd();
	int? prev = null;
	var count = 0;
	txt.Close();
	var list = txts.Split('\n').Select(x => int.Parse(x)).ToList();
	foreach (int i in list)
{
	if (prev != null)
	{
		count += (i - prev) > 0 ? 1 : 0;
	}
prev = i;
}
Console.WriteLine(count);
Console.WriteLine(list.Count);

List<int> window = new List<int>();

for (int j = 2; j < list.Count; j++)
{
	window.Add(list[j] + list[j - 1] + list[j - 2]);
}
int? pre = null;
int cnt = 0;
foreach (int x in window)
{
	if (pre != null)
	{
		cnt += (x - pre) > 0 ? 1 : 0;
	}
	pre = x;
}
Console.WriteLine(cnt);
Console.WriteLine(window.Count);
}

static void day2()
{
	StreamReader txt = new StreamReader(@"C:\Users\11349\source\repos\Advent\input2.txt");
	var txts = txt.ReadToEnd();
	txt.Close();
	List<string> commands = new List<String>(txts.Split('\n'));
	Console.WriteLine(commands[0]);

	IEnumerable<command> vs = from x in commands
							  where true
							  select new command(x: x.Split(' ').FirstOrDefault(), y: int.Parse(x.Split(' ').LastOrDefault()));

	List<command>? cmds= new List<command>(vs);
	int depth = 0;
	int breadth = 0;
foreach (command cmd in cmds)
	{
		switch (cmd.dir)
		{
	case "Forward":
	case "forward":

		breadth += cmd.dist;
	break;
	case "Up":
	case "up":
	depth -= cmd.dist;
	break;
	case "Down":
	case "down":
		depth += cmd.dist;
	break;
		}
	}

	int dpth = 0;
	int brdth = 0;
	int aim = 0;
	foreach (command comd in cmds)
	{
		switch (comd.dir)
		{
			case "Forward":
			case "forward":
				brdth += comd.dist;
				dpth += aim * comd.dist; 
				break;
			case "Up":
			case "up":
				aim -= comd.dist;
				break;
			case "Down":
			case "down":
				aim += comd.dist;
				break;
		}
	}
	Console.WriteLine($"Depth: {dpth} other: {brdth} aim: {aim}");
	Console.WriteLine(dpth*brdth);
}

static void day3()
{
	string[] txts = File.ReadAllLines(@"C:\Users\11349\source\repos\Advent\input3.txt");
	List<string> binary = new List<String>(txts);
	Console.WriteLine(binary[0]);
	var rows = binary.Count();
	var cols = binary[0].Count();
	List<int> coltotals = new List<int>();
	List<char> gamma = new List<char>();
	List<char> epsilon = new List<char>();
	for (int col = 0; col < cols; col++)
	{
		var coltotal = 0;
		for (int row = 0; row < rows; row++)
		{
			coltotal += binary[row][col] - '0';
		}
		coltotals.Add(coltotal);
		gamma.Add(coltotal < 500 ? '0' : '1');
		epsilon.Add(coltotal < 500 ? '1' : '0');
	}
	Console.WriteLine($"Total binary numbers: {rows}");
	int index = cols - 1;
	foreach (int c in coltotals)
	{
		Console.WriteLine($"Total of {Math.Pow(2, index)}: {c}"); index--;
	}
	var sgamma = new String(gamma.ToArray());
	var sepsilon = new String(epsilon.ToArray());
	Console.WriteLine($"Gamma: {sgamma} = {Convert.ToInt32(sgamma, 2)}");
	Console.WriteLine($"Epsilon: {sepsilon} = {Convert.ToInt32(sepsilon, 2)}");

	Console.WriteLine(Convert.ToInt32(sgamma, 2) * Convert.ToInt32(sepsilon, 2));
	Console.WriteLine("Part 2:");
	//Part 2
	//f-filter
	var frows = binary.Count();
	var fcols = binary[0].Count();
	var foxbinary = binary;
	List<char> ox = new List<char>();
	for (int fcol = 0; fcol < fcols; fcol++)
	{
		if (foxbinary.Count() <= 1)
			break;
		var fcoltotal = 0;
		for (int frow = 0; frow < frows; frow++)
		{
			fcoltotal += foxbinary[frow][fcol] - '0';
		}
		ox.Add(fcoltotal < (float)frows/2 ? '0' : '1');
		IEnumerable<string> temp = from x in foxbinary
								   where x[fcol] == ox[fcol]
								   select x;
		foxbinary = new List<string>(temp);
		frows = foxbinary.Count();
		Console.WriteLine(frows);
	}

	frows = binary.Count();
	fcols = binary[0].Count();
	var fcobinary = binary;
	List<char> co = new List<char>();

	for (int fcol = 0; fcol < fcols; fcol++)
	{
		if (fcobinary.Count() <= 1)
			break;
		var fcoltotal = 0;
		for (int frow = 0; frow < frows; frow++)
		{
			fcoltotal += fcobinary[frow][fcol] - '0';
		}
		co.Add(fcoltotal < (float)frows/2 ? '1' : '0');
		IEnumerable<string> temp = from x in fcobinary
								   where x[fcol] == co[fcol]
								   select x;
		fcobinary = new List<string>(temp);
		frows = fcobinary.Count();
	}

	Console.WriteLine($"Ox: {foxbinary}, {foxbinary.Count()}");

		Console.WriteLine($"Ox gen rating: {foxbinary[0]}. CO2 gen rating: {fcobinary[0]}");
	Console.WriteLine($"Life rating = {Convert.ToInt32(foxbinary[0],2) * Convert.ToInt32(fcobinary[0],2)}");


}

static void day4()
{
	StreamReader txt = new StreamReader(@"C:\Users\11349\source\repos\Advent\input4.txt");
	var numbers = txt.ReadLine()
		.Split(',')
		.Select(x => int.Parse(x))
		.ToList();
	numbers.ForEach(x => Console.Write(x+", "));
	var boards = txt.ReadToEnd()
		.Split("\r\n\r\n")
		.ToList<string>();
	txt.Close();
	int[][][] board = new int[boards.Count][][];
	for (int g = 0; g < boards.Count; g++) board[g] = new int[5][];
	int ignor = -1; int i = 0;
	foreach (var x in boards)
	{
		int j = 0;  Console.WriteLine($"230: {x}");
		var xx = x.Trim().Replace("  ", " ").Split('\n');
		foreach (var y in xx)
		{
			var a = String.Join("", y)
				.Split(' ')
				.Where(c => int.TryParse(c, out ignor))
				.Select(cs => int.Parse(cs))
				.ToArray();

			board[i][j++] = a;
		}
		i++;
	}
	Console.WriteLine(board[boards.Count() - 1][1][1]);
	Console.WriteLine($"{board[0][0][0]}, {board[0][0][1]}, {board[0][0][2]}, {board[0][0][3]}, {board[0][0][4]} \n" +
		$"{board[0][1][0]}, {board[0][1][1]}, {board[0][1][2]}, {board[0][1][3]}, {board[0][1][4]}\n" +
		$"{board[0][2][0]}, {board[0][2][1]}, {board[0][2][2]}, {board[0][2][3]}, {board[0][2][4]}\n" +
		$"{board[0][3][0]}, {board[0][3][1]}, {board[0][3][2]}, {board[0][3][3]}, {board[0][3][4]}\n" +
		$"{board[0][4][0]}, {board[0][4][1]}, {board[0][4][2]}, {board[0][4][3]}, {board[0][4][4]}\n");
	bool[][][] flag = new bool[boards.Count()][][];
	for (int ii = 0; ii < boards.Count(); ii++)
	{
		flag[ii] = new bool[5][];
		for (int jj = 0; jj< 5; jj++)
		{
			flag[ii][jj] = new bool[5];
			for (int kk = 0; kk < 5; kk++) flag[ii][jj][kk] = false;
		}
	}
	Console.WriteLine($"{flag[15][0][0]}, {flag[15][0][1]}, {flag[15][0][2]}, {flag[15][0][3]}, {flag[15][0][4]} \n" +
		$"{flag[15][1][0]}, {flag[15][1][1]}, {flag[15][1][2]}, {flag[15][1][3]}, {flag[15][1][4]}\n" +
		$"{flag[15][2][0]}, {flag[15][2][1]}, {flag[15][2][2]}, {flag[15][2][3]}, {flag[15][2][4]}\n" +
		$"{flag[15][3][0]}, {flag[15][3][1]}, {flag[15][3][2]}, {flag[15][3][3]}, {flag[15][3][4]}\n" +
		$"{flag[15][4][0]}, {flag[15][4][1]}, {flag[15][4][2]}, {flag[15][4][3]}, {flag[15][4][4]}\n");
	bool won = false;
	bool lastwon = false;
	bool alreadywon = false;
	var winnerthisnumber = false;
	var lastwinneri = -1;
	var lastsum = 0;
	var nextnumberindex = 0;
	var nextnumber = 0;
	var numberofnumbers = numbers.Count();
	var winneri = -1;
	var tilwin = numbers.Take(22).ToList();
	var sum = 0;
	var numberofboards = boards.Count();
	var listofwinners = new List<int>();
	while (!(won && lastwon) && (nextnumberindex < numberofnumbers))
	{
		nextnumber = numbers[nextnumberindex++];
		for (int nexti = 0; nexti < numberofboards; nexti++)
		{
			if (board[nexti].SelectMany(o => o).Any(oo => oo == nextnumber))
			{
				for (int nextj = 0; nextj < 5; nextj++)
				{
					if (Array.IndexOf(board[nexti][nextj], nextnumber) != -1)
						flag[nexti][nextj][Array.IndexOf(board[nexti][nextj], nextnumber)] = true;
				}
				for (int j1 = 0; j1 < 5; j1++)
				{
					if (!listofwinners.Contains(nexti))
					{
						if ((flag[nexti][j1][0] && flag[nexti][j1][1] && flag[nexti][j1][2] && flag[nexti][j1][3] && flag[nexti][j1][4]))
						{
							won = true;
							winneri = nexti;
							winnerthisnumber = true;
						}
						if (flag[nexti][0][j1] && flag[nexti][1][j1] && flag[nexti][2][j1] && flag[nexti][3][j1] && flag[nexti][4][j1])
						{
							won = true;
							winneri = nexti;
							winnerthisnumber = true;
						} 
					}
				}
				if (winnerthisnumber)
				{
					if(!listofwinners.Contains(nexti))
						listofwinners.Add(nexti);
					if (listofwinners.Count() == numberofboards)
					{
						lastwinneri = nexti;
						lastwon = true;
					}
					winnerthisnumber = false;
				}
			}
		}
		Console.WriteLine($"Nextnumberindex: {nextnumberindex}. Nextnumber: {nextnumber}. Lastwon: {lastwon}. Won: {won}. Winners: {listofwinners.Count()}. Numberofboards: {numberofboards}");
		listofwinners.ForEach(x => Console.Write($"{x},"));
		Console.WriteLine();
		//nextnumberindex++;
			if (won && !alreadywon)
			{
				sum = board[winneri].SelectMany(o => o).Sum();
				for (int i1 = 0; i1 < 5; i1++)
				{
					for (int i2 = 0; i2 < 5; i2++)
					{
						sum -= flag[winneri][i1][i2] ? board[winneri][i1][i2] : 0;
					}
				}
				alreadywon = true;
				numbers.Take(nextnumberindex).ToList().ForEach(x => Console.Write($"{x}, "));
				Console.WriteLine(winneri);
				Console.WriteLine(boards[winneri]);
				Console.WriteLine(sum);
				Console.WriteLine(nextnumber);
				Console.WriteLine(sum * nextnumber);
			}
			if (lastwon)
			{
				lastsum = board[lastwinneri].SelectMany(o => o).Sum();
				for (int i1 = 0; i1 < 5; i1++)
				{
					for (int i2 = 0; i2 < 5; i2++)
					{
						lastsum -= flag[lastwinneri][i1][i2] ? board[lastwinneri][i1][i2] : 0;
					}
				}
				numbers.Take(nextnumberindex).ToList().ForEach(x => Console.Write($"{x}, "));
				Console.WriteLine(lastwinneri);
				Console.WriteLine(boards[lastwinneri]);
				Console.WriteLine(lastsum);
				Console.WriteLine(nextnumber);
				Console.WriteLine(lastsum * nextnumber);
			}
	}
	Console.WriteLine($"{flag[lastwinneri][0][0]}, {flag[lastwinneri][0][1]}, {flag[lastwinneri][0][2]}, {flag[lastwinneri][0][3]}, {flag[lastwinneri][0][4]} \n" +
	   $"{flag[lastwinneri][1][0]}, {flag[lastwinneri][1][1]}, {flag[lastwinneri][1][2]}, {flag[lastwinneri][1][3]}, {flag[lastwinneri][1][4]}\n" +
	   $"{flag[lastwinneri][2][0]}, {flag[lastwinneri][2][1]}, {flag[lastwinneri][2][2]}, {flag[lastwinneri][2][3]}, {flag[lastwinneri][2][4]}\n" +
	   $"{flag[lastwinneri][3][0]}, {flag[lastwinneri][3][1]}, {flag[lastwinneri][3][2]}, {flag[lastwinneri][3][3]}, {flag[lastwinneri][3][4]}\n" +
	   $"{flag[lastwinneri][4][0]}, {flag[lastwinneri][4][1]}, {flag[lastwinneri][4][2]}, {flag[lastwinneri][4][3]}, {flag[lastwinneri][4][4]}\n");
}

static void day5()
{
	var txts = File.ReadAllLines(@"C:\Users\11349\source\repos\Advent\input5.txt").ToList();
	//txts.ForEach(x => Console.Write($"({x})"));
	Console.WriteLine(txts[0]);
	var tups = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>(from x in txts
			   select ((int.Parse(x.Split("->")[0].Split(',')[0]), int.Parse(x.Split("->")[0].Split(',')[1])).ToTuple(), (int.Parse(x.Split("->")[1].Split(',')[0]), int.Parse(x.Split("->")[1].Split(',')[1])).ToTuple()).ToTuple());
	Console.WriteLine($"Line from: {tups[0].Item1.Item1},{tups[0].Item1.Item2} to: : {tups[0].Item2.Item1},{tups[0].Item2.Item2}");

	//Func<char[][]> creategraph = size =>
	T[][] creategraph<T> (T arrayof, int size)
	{
		//var vs = new char[size][];
		//return new int[] Enumerable.Repeat<T>(arrayof, size).ToArray(); 
		T[][] flag = new T[size][];
		for (int ii = 0; ii < size; ii++)
		{
			flag[ii] = new T[size];
				for (int kk = 0; kk < size; kk++) flag[ii][kk] = arrayof;
		}
		return flag;
	}

	// Wrong! Action<int, int, T[][]> printgraph = (int a, int b, T[][] vs) => Wrong!
	void printgraph<T>(int sizex,int sizey,T[][] graphtoprint)
	{
		for (int x1 = 0; x1 < sizex; x1++)
		{
			for (int y1 = 0; y1 < sizey; y1++)
			{
				Console.Write(graphtoprint[x1][y1]);
			}
			Console.WriteLine();
		}
	};

	//void printline(<Tuple<Tuple<int, int>, Tuple<int, int>>> tup)
	Action<Tuple<Tuple<int, int>, Tuple<int, int>>> printline = tup =>
{
		   var fromx = tup.Item1.Item1;
		   var fromy = tup.Item1.Item2;
		   var tox = tup.Item2.Item1;
		   var toy = tup.Item2.Item2;
		   Console.WriteLine($"Line from: {fromx},{fromy} to: {tox},{toy}");
	   };

	//Action<Tuple<Tuple<int, int>, Tuple<int, int>>> addline = tup =>
	void addline( Tuple<Tuple<int, int>, Tuple<int, int>> tup,int[][] graphtoadd)
	{
		var fromx = tup.Item1.Item1;
		var fromy = tup.Item1.Item2;
		var tox = tup.Item2.Item1;
		var toy = tup.Item2.Item2;
		Console.WriteLine($"Line from: {fromx},{fromy} to: {tox},{toy}");
		var difx = fromx - tox;
		int dify = fromy - toy;
		var signx = difx >= 0;
		var signy = dify >= 0;
		var hori = dify == 0;
		var vert = difx == 0;
		var diag = Math.Abs(difx) == Math.Abs(dify);
		var startx = -1;
		var starty = -1;
		var endx = -1;
		var endy = -1;
		Console.WriteLine($"Horizontal: {hori} or Vertical: {vert} or Diagonal {diag}");
		if (hori)
		{
			if (signx)
			{
				startx = tox;
				endx = fromx;
			}
			else
			{
				startx = fromx;
				endx = tox;
			}
			for (int i = startx; i <= endx; i++)
			{
				graphtoadd[i][fromy] += 1;
			}
		}
		if (vert)
		{
			if (signy)
			{
				starty = toy;
				endy = fromy;
			}
			else
			{
				starty= fromy;
				endy = toy;
			}
			for (int j = starty; j <= endy; j++)
			{
				graphtoadd[fromx][j] += 1;
			}
		}
		if (diag)
		{
			int k =fromy;
			int j = fromx;
			var fin = false;
			while (true)
			{
				
				Console.WriteLine($"endx: {tox} endy: {toy}");
				graphtoadd[j][k] += 1;
				Console.WriteLine($"x: {j} y: {k}");
				if (signy && signx)
				{// -/
					k--;
					j--;
				}
				if (signy && !signx)
				{// -\
					k--;
					j++;
				}
				if (!signy && signx)
				{// +\
					k++;
					j--;
				}
				if (!signy && !signx)
				{// +/
					k++;
					j++;
				}
				if ((j == tox) || (k == toy))
					fin = true;
				if (fin && (j != tox || k != toy))
					break;
			}            
		}

	};
	int countvents(int size,int[][] graphtocount)
	{
		int sum = 0;
		for (int sizex1 = 0; sizex1 < size; sizex1++)
		{
			for (int sizex2 = 0;sizex2 < size; sizex2++)
			{
				sum += graphtocount[sizex1][sizex2] > 1 ? 1 : 0;
			}
		}
		return sum;
	}
	//var graph = creategraph<int>(0, 10);
	//printgraph<int>(10, 10, graph);
	//addline(((1, 1).ToTuple(), (3, 3).ToTuple()).ToTuple(), graph);
	//addline(((6, 6).ToTuple(), (3, 3).ToTuple()).ToTuple(), graph);
	//addline(((9, 3).ToTuple(), (3, 9).ToTuple()).ToTuple(), graph);
	//addline(((1, 5).ToTuple(), (3, 3).ToTuple()).ToTuple(), graph);
	//graph = creategraph<int>(0, 10);
	//    addline(((9, 9).ToTuple(), (0, 0).ToTuple()).ToTuple(), graph);
	//    printgraph<int>(10, 10, graph);
	//    graph = creategraph<int>(0, 10);
	//    addline(((0, 0).ToTuple(), (9, 9).ToTuple()).ToTuple(), graph);
	//    printgraph<int>(10, 10, graph);
	//graph = creategraph<int>(0, 10);
	//    addline(((9, 0).ToTuple(), (0, 9).ToTuple()).ToTuple(), graph);
	//    printgraph<int>(10, 10, graph);
	//    graph = creategraph<int>(0, 10);
	//    addline(((0, 9).ToTuple(), (9, 0).ToTuple()).ToTuple(), graph);
	//    printgraph<int>(10, 10, graph);

	var graph = creategraph<int>(0, 1000);
	foreach (var item in tups)
	{
		addline(item, graph);
	}
	Console.WriteLine(countvents(1000, graph));
}

static void day6()
{
	StreamReader txt = new StreamReader(@"C:\Users\11349\source\repos\Advent\input6.txt");
	List<byte>? fishes = txt.ReadLine()
							.Split(',')
							.Select(x => byte.Parse(x))
							.ToList();

	long[] fish = new long[9];
	fish[0] = fishes.Where(x => x == 0).ToList().Count();
	fish[1] = fishes.Where(y => y == 1).ToList().Count();
	fish[2] = fishes.Where(z => z == 2).ToList().Count();
	fish[3] = fishes.Where(w => w == 3).ToList().Count();
	fish[4] = fishes.Where(v => v == 4).ToList().Count();
	fish[5] = fishes.Where(u => u == 5).ToList().Count();
	fish[6] = fishes.Where(t => t == 6).ToList().Count();
	fish[7] = fishes.Where(s => s == 7).ToList().Count();
	fish[8] = fishes.Where(r => r == 8).ToList().Count();

	void oldnextday(List<byte> xx)
	{

		for (int i1 = 0; i1 < xx.Count; i1++)
		{
			if (xx[i1] == 0)
			{
				xx[i1] = 6;
				xx.Add(9);
			}
			else
				xx[i1]--;
		}

	}
	Dictionary<byte,int> countfishes()
	{
		var dict = fishes.GroupBy(s => s)
  .Select(s => new
  {
	  Age = s.Key,
	  Count = s.Count()
  }).ToDictionary(g => g.Age, g => g.Count);
		return dict;
	}

	void nextday()
	{
		var toadd = fish[0];
		for (int i1 = 0; i1 < fish.Length; i1++)
		{
			if (i1 < fish.Length-1)
				fish[i1] = fish[i1 + 1];
			else
				fish[i1] = 0;
		}
		fish[6] += toadd;
		fish[8] += toadd;
	}

	void oldfastforward(List<byte> xx, int days, bool print)
	{
		for (int i = 0; i < days; i++)
		{
			oldnextday(xx);
			if (print)
			{
				Console.WriteLine($"Day {i}:");
				printanything(countfishes());
			}
			Console.WriteLine(fishes.Count);
		}
	}
	
	long totalfish()
	{
		long x = 0;
		foreach (long xx in fish)
			x += xx;
		return x;
	}

	void fastforward( int days, bool print)
	{
		for (int i = 0; i < days; i++)
		{
			nextday();
			if (print)
			{
				Console.WriteLine($"Day {i}:");
				printanything(fish);
			}
			Console.WriteLine(totalfish());
		}
	}

	for (int i = 0; i < 1; i++)
	{
		Console.WriteLine($"Day {i}: ");
		oldfastforward(fishes, 1, true);
		fastforward(1, true); 
	}
	fastforward(256, false);
}

static void day7()
{
	StreamReader txt = new StreamReader(@"C:\Users\11349\source\repos\Advent\input7.txt");
	List<int>? crabs = txt.ReadLine()
							.Split(',')
							.Select(x => int.Parse(x))
							.ToList();
	var test = new List<int>() { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };

	int getmedian(List<int> vs)
	{
		vs.Sort();
		return vs[(int)(vs.Count / 2)]; 
	}

	int getfuelcost(int a)
	{
		if (a == 1 || a==0)
			return a;
		return (a * (a+1))/2;
	}

	void compute(List<int> vs,bool avg)
	{


		printanything(vs);

		if (!avg)
		{
			var median = getmedian(vs);
			Console.WriteLine($"Median = {median}.");
			var meddifference = vs.Select(x => Math.Abs(x - median)).ToList();
			printanything(meddifference);
			Console.WriteLine($"Total fuel = {meddifference.Sum()}");  
		}
		else
		{
			var average = (int)(vs.Average());
			Console.WriteLine($"Average = {vs.Average()}.");
			var avgdifference = vs.Select(x => Math.Abs(x - average)).ToList();
			printanything(avgdifference);
			Console.WriteLine($"Total fuel = {avgdifference.Select(p => getfuelcost(p)).ToList().Sum()}");
		}
	}
	compute(test,true);
	compute(crabs, true);

}

static void day8()
{
	var txts = File.ReadAllLines(@"C:\Users\11349\source\repos\Advent\input8.txt").ToList();

//    txts = new List<string>(){ "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb |fdgacbe cefdb cefbgd gcbe","edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec |fcgedb cgb dgebacf gc",
//"fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef |cg cg fdcagb cbg",
//"fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega |efabcd cedba gadfec cb",
//"aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga |gecf egdcabf bgf bfgea",
//"fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf |gebdcfa ecba ca fadegcb",
//"dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf |cefg dcbef fcge gbcadfe",
//"bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd |ed bcgafe cdgba cbgef",
//"egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg |gbdfcae bgc cg cgb",
//"gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc |fgae cfgab fg bagce"};
	int lines = txts.Count;
	var indigits = txts.Select(x => x.Trim().Split('|')[0].Trim()).ToList().Select(x=>x.Split(' ').ToList()).ToList();
	var outdigits = txts.Select(x => x.Trim().Split('|')[1].Trim()).ToList().Select(x=>x.Split(' ').ToList()).ToList();
	void part1()
	{
		var part1count = outdigits.SelectMany(o => o).Where(o => o.Length == 2 || o.Length == 3 || o.Length == 4 || o.Length == 7).Count();
		Console.WriteLine(part1count); 
	}

	void part2()
	{
		//Dictionary<char, List<int>> expnum = new Dictionary<char, List<int>>();
		//expnum['a'] = new List<int>() { 0,    2, 3,    5, 6, 7, 8, 9 };//8
		//expnum['b'] = new List<int>() { 0,          4, 5, 6,    8, 9 };//6
		//expnum['c'] = new List<int>() { 0, 1, 2, 3, 4,       7, 8, 9 };//8
		//expnum['d'] = new List<int>() {       2, 3, 4, 5, 6,    8, 9 };//7
		//expnum['e'] = new List<int>() { 0,    2,          6,    8    };//4
		//expnum['f'] = new List<int>() { 0, 1,    3, 4, 5, 6, 7, 8, 9 };//9
		//expnum['g'] = new List<int>() { 0,    2, 3,    5, 6,    8, 9 };//7

		//Dictionary<int, HashSet<char>> expfig = new Dictionary<int, HashSet<char>>();
		////Unique Lengths for 1(2), 4(4), 7(3), 8(7).
		//expfig[1] = new HashSet<char>() {           'c',           'f'      };//2
		//expfig[4] = new HashSet<char>() {      'b', 'c', 'd',      'f'      };//4
		//expfig[7] = new HashSet<char>() { 'a',      'c',           'f'      };//3
		//expfig[8] = new HashSet<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };//7
		////Lengths of 5
		//expfig[2] = new HashSet<char>() { 'a',      'c', 'd', 'e',      'g' };//5        
		//expfig[3] = new HashSet<char>() { 'a',      'c', 'd',      'f', 'g' };//5
		//expfig[5] = new HashSet<char>() { 'a', 'b',      'd',      'f', 'g' };//5
		////Lengths of 6
		//expfig[0] = new HashSet<char>() { 'a', 'b', 'c',      'e', 'f', 'g' };//6
		//expfig[6] = new HashSet<char>() { 'a', 'b',      'd', 'e', 'f', 'g' };//6
		//expfig[9] = new HashSet<char>() { 'a', 'b', 'c', 'd',      'f', 'g' };//6
		Dictionary<char,int> actnum = new Dictionary<char, int>() { { 'a', 0 }, { 'b' , 0 }, { 'c' , 0 }, { 'd', 0 }, { 'e', 0 }, { 'f', 0 }, { 'g', 0 } };
		Dictionary<int, HashSet<char>> actfig = new Dictionary<int, HashSet<char>>();
		int totaloutput = 0;

		foreach(var (line,outline) in indigits.Zip(outdigits))
		{
			Console.WriteLine($"Line {indigits.IndexOf(line)}");
			foreach(var ch in actnum)
			{
				actnum[ch.Key] = line.SelectMany(o => o).Where(o => o == ch.Key).Count();
			}
			printanything(line);
			foreach (var num in actnum)
			{
				Console.WriteLine($"{num.Key}: {num.Value}");
			}
			actfig[1] = line.FirstOrDefault(o => o.Length == 2).ToHashSet();
			actfig[7] = line.FirstOrDefault(o => o.Length == 3).ToHashSet();
			actfig[4] = line.FirstOrDefault(o => o.Length == 4).ToHashSet();
			actfig[8] = line.FirstOrDefault(o => o.Length == 7).ToHashSet();
															   
			foreach (var word in line)
			{
				var charset = word.ToHashSet();
				switch (word.Length)
				{
					case 5:
						//length of five with [2,3,5]
						if (!word.Contains(actnum.FirstOrDefault(o=>o.Value == 9).Key))
							//only 2 does not contain the f segment. The new name of the f segment can be retrieved by counting the most common segment
							actfig[2] = charset;
						else if (actfig[7].IsSubsetOf(charset))
							actfig[3] = charset;
						else
							actfig[5] = charset;
						break;
					case 6:
						//length of six with [0,6,9]
						if (!word.Contains(actnum.FirstOrDefault(o => o.Value == 4).Key))
							actfig[9] = charset;
						else if(word.Where(o=> actnum.Where(o => o.Value == 8).Select(o=>o.Key).Contains(o)).Count() == 2)
							actfig[0] = charset;
						else if (word.Where(o => actnum.Where(o => o.Value == 7).Select(o => o.Key).Contains(o)).Count() == 2)
							actfig[6] = charset;
							break;
					default: 
						break;
				}
			}
			int lineoutput = 0;
			lineoutput += actfig.FirstOrDefault(o => outline[3].ToHashSet().SetEquals(o.Value)).Key;
			lineoutput += actfig.FirstOrDefault(o => outline[2].ToHashSet().SetEquals(o.Value)).Key*10;
			lineoutput += actfig.FirstOrDefault(o => outline[1].ToHashSet().SetEquals(o.Value)).Key*100;
			lineoutput += actfig.FirstOrDefault(o => outline[0].ToHashSet().SetEquals(o.Value)).Key*1000;
			totaloutput += lineoutput;
		}
		Console.WriteLine(totaloutput);
	}
	//   aaaa  
	//  b    c
	//  b    c
	//   dddd 
	//  e    f
	//  e    f
	//   gggg

	void compute()
	{
		printanything(indigits[0]);
		printanything(outdigits[0]);
		part1();
		Console.WriteLine("---------------------------------------------------------------------");
		part2();
	}
	compute();
}

static void day9()
{
	StreamReader txt = new StreamReader(@"C:\Users\11349\source\repos\Advent\input9.txt");
	var heightmap = new List<List<int>>();
	heightmap.Add(txt.ReadLine().Select(o => ((int)o - 48)).ToList());
	while (!txt.EndOfStream)
	{
		heightmap.Add(txt.ReadLine().Select(o => ((int)o - 48)).ToList());
	}
	txt.Close();
//    heightmap = @"2199943210
//3987894921
//9856789892
//8767896789
//9899965678".Split('\n').Select(o=>o.Select(x=>(int)x-48).ToList()).ToList();
	var heightarray = heightmap.Select(o => o.ToArray<int>()).ToArray<int[]>();
	var lowestpoints = new List<Tuple<int, int>>();
	bool islowestpoint(int x, int y)
	{
		bool islowest = true;
		if (x == 0 && y == 0)
		{
			islowest &= heightarray[x + 1][y] > heightarray[x][y];
			islowest &= heightarray[x][y + 1] > heightarray[x][y];
		}
		if (x == heightarray.GetLength(0) - 1 && y == 0)
		{
			islowest &= heightarray[x - 1][y] > heightarray[x][y];
			islowest &= heightarray[x][y + 1] > heightarray[x][y];
		}
		if (x == 0 && y == heightarray[0].GetLength(0) - 1)
		{
			islowest &= heightarray[x + 1][y] > heightarray[x][y];
			islowest &= heightarray[x][y - 1] > heightarray[x][y];
		}
		if (x == heightarray.GetLength(0) - 1 && y == heightarray[0].GetLength(0) - 1)
		{
			islowest &= heightarray[x - 1][y] > heightarray[x][y];
			islowest &= heightarray[x][y - 1] > heightarray[x][y];
		}
		if (y==0 && (x!=0 && x!= heightarray.GetLength(0) - 1))
		{
			islowest &= heightarray[x][y + 1] > heightarray[x][y];
			islowest &= heightarray[x-1][y] > heightarray[x][y];
			islowest &= heightarray[x+1][y] > heightarray[x][y]; 
		} 
		if (x==0 && (y!=0 && y!= heightarray[0].GetLength(0) - 1))
		{
			islowest &= heightarray[x][y + 1] > heightarray[x][y];
			islowest &= heightarray[x][y-1] > heightarray[x][y];
			islowest &= heightarray[x + 1][y] > heightarray[x][y];
		} 
		if (x==heightarray.GetLength(0)-1&&(y!=0 && y!= heightarray[0].GetLength(0) - 1))
		{
			islowest &= heightarray[x][y + 1] > heightarray[x][y];
			islowest &= heightarray[x][y - 1] > heightarray[x][y];
			islowest &= heightarray[x - 1][y] > heightarray[x][y];
		}
		if (y == heightarray[0].GetLength(0)-1 && (x!=0 && x!= heightarray.GetLength(0) - 1))
		{
			islowest &= heightarray[x][y - 1] > heightarray[x][y];
			islowest &= heightarray[x - 1][y] > heightarray[x][y];
			Console.WriteLine($"({x},{y}) of ({heightarray.GetLength(0)-1},{heightarray[0].GetLength(0)-1})");
			islowest &= heightarray[x + 1][y] > heightarray[x][y];
		}
		if (y!=0 && x!=0 && y!= heightarray[0].GetLength(0) - 1 && x!= heightarray.GetLength(0) - 1)
		{
			islowest &= heightarray[x + 1][y] > heightarray[x][y];
			islowest &= heightarray[x - 1][y] > heightarray[x][y];
			islowest &= heightarray[x][y - 1] > heightarray[x][y];
			islowest &= heightarray[x][y + 1] > heightarray[x][y]; 
		}
		return islowest;
	}
	int part1()
	{
		for (int i = 0; i < heightarray.GetLength(0); i++)
		{
			for (int j = 0; j < heightarray[0].GetLength(0); j++)
			{
		if (islowestpoint(i,j))
		{
			lowestpoints.Add((i, j).ToTuple());
		}
			}
		}
		var risktotal = 0;
		foreach (var (x, y) in lowestpoints)
		{
			risktotal += (heightarray[x][y] + 1);
		}
		return risktotal;
	}

	var basins = new Dictionary<(int, int), List<(int, int)>>();
	void part2()
	{
		void recursivesearch(int x, int y,int ox,int oy)
		{
			if (heightarray[x][y] != 9 && !basins[(ox,oy)].Contains((x,y)))
			{
				var val = heightarray[x][y];
					basins[(ox, oy)].Add((x, y));
					if (x == 0 && y == 0)
					{
						if (heightarray[x + 1][y] > val) recursivesearch(x + 1, y, ox, oy); if ( heightarray[x][y + 1] > val) recursivesearch(x, y+1, ox, oy);
					}
					if (x == heightarray.GetLength(0) - 1 && y == 0)
					{
						if ( heightarray[x - 1][y] > val) recursivesearch(x - 1, y, ox, oy); if ( heightarray[x][y + 1] > val) recursivesearch(x , y+1, ox, oy);
					}
					if (x == 0 && y == heightarray[0].GetLength(0) - 1)
					{
						if ( heightarray[x + 1][y] > val) recursivesearch(x + 1, y, ox, oy); if ( heightarray[x][y - 1] > val) recursivesearch(x , y-1, ox, oy);
					}
					if (x == heightarray.GetLength(0) - 1 && y == heightarray[0].GetLength(0) - 1)
					{
						if ( heightarray[x - 1][y] > val) recursivesearch(x - 1, y, ox, oy); if ( heightarray[x][y - 1] > val) recursivesearch(x, y-1, ox, oy);
					}
					if (y == 0 && (x != 0 && x != heightarray.GetLength(0) - 1))
					{
						if ( heightarray[x][y + 1] > val) recursivesearch(x, y+1, ox, oy); if ( heightarray[x - 1][y] > val) recursivesearch(x - 1, y, ox, oy); if ( heightarray[x + 1][y] > val) recursivesearch(x + 1, y, ox, oy);
					}
					if (x == 0 && (y != 0 && y != heightarray[0].GetLength(0) - 1))
					{
						if ( heightarray[x][y + 1] > val) recursivesearch(x, y+1, ox, oy); if ( heightarray[x][y - 1] > val) recursivesearch(x, y-1, ox, oy); if ( heightarray[x + 1][y] > val) recursivesearch(x + 1, y, ox, oy);
					}
					if (y == heightarray[0].GetLength(0) - 1 && (x != 0 && x != heightarray.GetLength(0) - 1))
					{
						if ( heightarray[x][y - 1] > val) recursivesearch(x, y-1, ox, oy); if ( heightarray[x - 1][y] > val) recursivesearch(x -1, y, ox, oy); if ( heightarray[x + 1][y] > val) recursivesearch(x + 1, y, ox, oy);
					}
					if (x == heightarray.GetLength(0) - 1 && (y != 0 && y != heightarray[0].GetLength(0) - 1))
					{
						if ( heightarray[x][y + 1] > val) recursivesearch(x, y+1, ox, oy); if ( heightarray[x][y - 1] > val) recursivesearch(x, y-1, ox, oy); if ( heightarray[x - 1][y] > val) recursivesearch(x - 1, y, ox, oy);
					}
					if (y != 0 && x != 0 && y != heightarray[0].GetLength(0) - 1 && x != heightarray.GetLength(0) - 1)
					{
						if ( heightarray[x + 1][y] > val) recursivesearch(x + 1, y, ox, oy); if ( heightarray[x - 1][y] > val) recursivesearch(x - 1, y, ox, oy); if ( heightarray[x][y - 1] > val) recursivesearch(x, y-1, ox, oy); if ( heightarray[x][y + 1] > val) recursivesearch(x, y+1, ox, oy);
					}
			}
		}
		foreach (var (x,y) in lowestpoints)
		{
			basins[(x, y)] = new List<(int, int)>();
			recursivesearch(x,y,x,y);
		}
		var sizeofbasins = basins.Select(o => o.Value.Count()).ToList();
		sizeofbasins.Sort();
		sizeofbasins.Reverse();
		var parttwo = sizeofbasins.Take(3).Aggregate((acc,x)=>acc*x);
		//foreach (var basin in basins)
		//{
		//    printanything(basin.Value);
		//}
		printanything(sizeofbasins);
		Console.WriteLine( parttwo );
	}
	void compute()
	{
		var risktotal = part1();
		Console.WriteLine(risktotal);
		part2();
	}
	compute();
}

static void day10()
{
	var txts = File.ReadAllLines(@"C:\Users\11349\source\repos\Advent\input10.txt").ToList();
	var totalscope = 0;
	var part1sum = 0;

	List<List<char>> heirarchy = new List<List<char>>();
	for(int i = 0; i < 17; i++)
	{
		heirarchy.Add(new List<char>());
	}

	void addspaces (int i)
	{
		for (int y=0; y < heirarchy.Count; y++)
		{
			if (y != i)
				heirarchy[y].Add(' ');
		}
	}

	void traverseheirarchy(char c)
	{
		switch (c)
		{
			case '(':
				heirarchy[totalscope].Add('(');
				addspaces(totalscope);
				totalscope++;
				break;
			case ')':
				totalscope--;
				heirarchy[totalscope].Add(')');
				addspaces(totalscope);
				break;
		   
			case '[':
				heirarchy[totalscope].Add('[');
				addspaces(totalscope);
				totalscope++;
				break;
			case ']':
				totalscope--;
				heirarchy[totalscope].Add(']');
				addspaces(totalscope);
				break;
			
			case '{':
				heirarchy[totalscope].Add('{');
				addspaces(totalscope);
				totalscope++;
				break;
			case '}':
				totalscope--;
				heirarchy[totalscope].Add('}');
				addspaces(totalscope);
				break;
			
			case '<':
				heirarchy[totalscope].Add('<');
				addspaces(totalscope);
				totalscope++;
				break;
			case '>':
				totalscope--;
				heirarchy[totalscope].Add('>');
				addspaces(totalscope);
				break;
		}
	}

	bool iscorruptline(List<List<char>> architecture)
	{
		var corrupt = false;

		if (architecture.Where(o => o.Where(x => x != ' ').Count() % 2 == 1).Any())
			Console.WriteLine($"Incomplete error");
		var firsterror = (0, architecture[0].Count-1);
		for (int e=architecture.Count-1;e>=0;e--)
		{
			var line = architecture[e];
			for (int z = 0; z < line.Count-1; z++)
			{
				var char1 = line[z];
				var space = 1;
				if (!(new List<char>() { '(', '[', '{', '<' }.Contains(char1)))
				{
						continue; 
				}
				while(line[z+space] == ' ' && z + space < line.Count()-1) 
				{
					space++;
				}
				var char2 = line[z + space];
				if (char1 == '(' && (char2 == ']' || char2 == '}' || char2 == '>'))
				{
					Console.WriteLine($"at scope {e}:corruption error. Expected \')\' but recieved \'{char2}\'");
					firsterror = z+space < firsterror.Item2 ? (e,z+space) : firsterror;
					corrupt= true;
				}
				if (char1 == '[' && (char2 == ')' || char2 == '}' || char2 == '>'))
				{
					Console.WriteLine($"at scope {e}: corruption error. Expected \']\' but recieved \'{char2}\'");
					firsterror = z+space < firsterror.Item2 ? (e, z + space) : firsterror;
					corrupt = true;
			}
				if (char1 == '{' && (char2 == ')' || char2 == ']' || char2 == '>'))
				{
					Console.WriteLine($"at scope {e}: corruption error. Expected \'}}\' but recieved \'{char2}\'");
					firsterror = z+space < firsterror.Item2 ? (e,z+space) : firsterror;
					corrupt = true; 
				}
				if (char1 == '<' && (char2 == ')' || char2 == ']' || char2 == '}'))
				{
					Console.WriteLine($"at scope {e}: corruption error. Expected \'>\' but recieved \'{char2}\'");
					firsterror = z+space < firsterror.Item2 ? (e,z+space) : firsterror;
					corrupt = true; 
				}
			} 
		}
		if (corrupt)
		{
			var char3 = architecture[firsterror.Item1][firsterror.Item2];
			part1sum += char3 == ')' ? 3 : char3 == '}' ? 1197 : char3 == ']' ? 57 : 25137; 
		}
		return corrupt;
	}
	Tuple<List<char>,long> part2(List<List<char>> vs)
	{
		List<char> complete = new();
		long completescore = 0;
		for (int e = vs.Count - 1; e >= 0; e--)
		{
			var line = vs[e];
			for (int z = 0; z < line.Count; z++)
			{
				var char1 = line[z];
				var space = z == line.Count-1 ? 0: 1;
				if(z==21)
				Console.WriteLine($"scopeof: {e}. line length: {line.Count} and z value: {z}. and spaces: {space}. Currentchar= {char1}. next char= {line[z+space]}");
				if (!(new List<char>() { '(', '[', '{', '<'}.Contains(char1)))
					continue;
				while (line[z + space] == ' ' && z + space < line.Count - 1)
				{
					space++;
				}
			Console.WriteLine($"line length: {line.Count} and z value: {z}. and spaces: {space}");
				var char2 = z==line.Count-1 ? '\n':line[z + space];

				if (char1 == '(' && (char2 == ' ' || char2 == '\n'))
				{
					Console.WriteLine($"at scope {e}:incomplete error. added \')\'");
					complete.Add(')');
					completescore = completescore * (long)5 + (long)1;
				}
				if (char1 == '[' && (char2 == ' '||char2 =='\n'))
				{
					Console.WriteLine($"at scope {e}: incomplete error. added \']\'");
					complete.Add(']');
					completescore = completescore * (long)5 + (long)2;
				}
				if (char1 == '{' && (char2 == ' ' || char2 == '\n'))
				{
					Console.WriteLine($"at scope {e}: incomplete error. added \'}}\'");
					complete.Add('}');
					completescore = completescore * (long)5 + (long)3;
				}
				if (char1 == '<' && (char2 == ' ' || char2 == '\n'))
				{
					Console.WriteLine($"at scope {e}: incomplete error. added \'>\'");
					completescore = completescore * (long)5 + (long)4;
					complete.Add('>');
				}
			}
		}
		return (complete,completescore).ToTuple();
	}

	void compute()
	{
		part1sum = 0;
		int noofcorruptlines = 0;
		int totallines = txts.Count;
		List<List<char>> completionstrings = new();
		List<long> completionscores = new();
		foreach (var chunks in txts)//"[({(<(())[]>[[{[]{<()<>>\n[(()[<>])]({[<{<<[]>>(\n{([(<{}[<>[]}>{[]{[(<()>\n(((({<>}<{<{<>}{[]{[]{}\n[[<[([]))<([[{}[[()]]]\n[{[{({}]{}}([{[{{{}}([]\n{<[[]]>}<{[{[{[]{()[[[]\n[<(<(<(<{}))><([]([]()\n<{([([[(<>()){}]>(<<{{\n<{([{{}}[<[[[<>{}]]]>[]]".Split('\n'))  
		{
			foreach (char x in chunks)
			{
				traverseheirarchy(x);
			}
			for (int i = 0; i < heirarchy.Count; i++)
			{
				printanything(heirarchy[i]);
			}
			if (!iscorruptline(heirarchy))
			{
				var (autocomplete,part2s) = part2(heirarchy);
				completionstrings.Add(autocomplete);
				completionscores.Add(part2s);
				printanything(autocomplete);
			}
			else
				noofcorruptlines++;
			totalscope = 0;
			foreach (var list in heirarchy)
				list.Clear();
		}
		var middle = (completionscores.Count() - 1) / 2;
		var part2sum = completionscores.OrderBy(o=>o).ToList()[middle];
		Console.WriteLine(part1sum);
		Console.WriteLine($"corruptlines: {noofcorruptlines}. Totallines: {totallines}. incompletelines: {completionscores.Count()}. middle {middle}");
		for (int i = 0; i < completionscores.Count; i++)
		{
			Console.WriteLine($"{string.Join(',',completionstrings[i])} has score {completionscores[i]}.");
		}
		printanything(completionscores.OrderBy(p => p).ToList());
		Console.WriteLine(part2sum);
	}
	compute();
}

static void day11()
{
	bool test = true;
	List<List<int>> testinput = @"5483143223,2745854711,5264556173,6141336146,6357385478,4167524645,2176841721,6882881134,4846848554,5283751526".Split(',').Select(x => x.Select(o => o - '0').ToList()).ToList(); 
	var txts = new StreamReader(@"C:\Users\11349\source\repos\Advent\input11.txt").ReadToEnd();
	List<List<int>> octo = test? testinput : txts.Split('\n').Select(x => x.Trim().Select(o=>o-'0').ToList()).ToList();

	int pointstep(int x,int y,List<List<int>> points)
	{
		var countflashes = 0;
		var val = points[y][x];
		if (val < 9)
        {
            //Console.WriteLine($"Value now: {val}. new value: {points[y][x]+1}");
			points[y][x]++;
			return countflashes;
        }
		if(val == 10)
        {
			return countflashes;
        }
		if (val == 9)
        {
		points[y][x] = 10;
		countflashes++;
            foreach (var cc in new List<int>(){ -1, 0, 1 })
			{
				foreach (var rr in new List<int>() { -1, 0, 1 })
				{
					var r = x+rr;
					var c = y+cc;
					if (0 <= c && c < 10 && 0 <= r && r < 10 && points[c][r] != 10)
						countflashes += pointstep(r, c, points);
				}
			}
		//if (x == 0 && y == 0)
		//{
		//	//Console.WriteLine($"Top left corner. X:{x} Y:{y}");
		//	countflashes += pointstep(x + 1, y, points); countflashes += pointstep(x , y + 1, points); countflashes += pointstep(x + 1, y + 1, points);
		//}
		//if (x == 9 && y == 0)
		//{
		//	//Console.WriteLine($"Top right corner. X:{x} Y:{y}");
		//	countflashes += pointstep(x - 1, y, points); countflashes += pointstep(x , y + 1, points); countflashes += pointstep(x - 1, y + 1, points);
		//}
		//if (x == 0 && y == 9)
		//{
		//	//Console.WriteLine($"Bottom left corner. X:{x} Y:{y}");
		//	countflashes += pointstep(x + 1, y, points); countflashes += pointstep(x, y - 1, points); countflashes += pointstep(x + 1, y - 1, points);
		//}
		//if (x == 9 && y == 9)
		//{
		//	//Console.WriteLine($"Bottom right corner. X:{x} Y:{y}");
		//	countflashes += pointstep(x - 1, y, points); countflashes += pointstep(x, y - 1, points); countflashes += pointstep(x - 1, y - 1, points);
		//}
		//if (y == 0 && (x != 0 && x != 9))
		//{
		//	//Console.WriteLine($"Top edge. X:{x} Y:{y}");
		//	countflashes += pointstep(x , y + 1, points); countflashes += pointstep(x - 1, y, points);  countflashes += pointstep(x + 1, y, points); countflashes += pointstep(x - 1, y + 1, points); countflashes += pointstep(x + 1, y + 1, points);
		//}
		//if (x == 0 && (y != 0 && y != 9))
		//{
		//	 countflashes += pointstep(x , y + 1, points); countflashes += pointstep(x, y - 1, points);  countflashes += pointstep(x + 1, y, points); countflashes += pointstep(x + 1, y - 1, points); countflashes += pointstep(x + 1, y + 1, points);
		//}
		//if (y == 9 && (x != 0 && x != 9))
		//{
		//	 countflashes += pointstep(x, y - 1, points); countflashes += pointstep(x - 1, y, points); countflashes += pointstep(x + 1, y, points); countflashes += pointstep(x + 1, y - 1, points); countflashes += pointstep(x - 1, y - 1, points);
		//}
		//if (x == 9 && (y != 0 && y != 9))
		//{
		//	 countflashes += pointstep(x , y + 1, points);  countflashes += pointstep(x, y - 1, points); countflashes += pointstep(x - 1, y, points); countflashes += pointstep(x - 1, y + 1, points); countflashes += pointstep(x - 1, y - 1, points);
		//}
		//if (y != 0 && x != 0 && y != 9 && x != 9)
		//{
		//	countflashes += pointstep(x + 1, y, points); countflashes += pointstep(x - 1, y, points);  countflashes += pointstep(x, y - 1, points); countflashes += pointstep(x , y + 1, points); countflashes += pointstep(x + 1, y + 1, points); countflashes += pointstep(x - 1, y - 1, points); countflashes += pointstep(x - 1, y + 1, points); countflashes += pointstep(x + 1, y - 1, points);
		//	}
        }
		return countflashes;
	}

	int step(List<List<int>> points, bool toprint=true,int stepnumber=0)
	{
		var b4 = points;
		var flashes = 0;
		for (int line = 0; line < points.Count; line++)
		{
			for (int item = 0; item < octo[0].Count; item++)
				flashes += pointstep(item, line, points);
		}
		if (flashes == 100)
			return -1;

		for (int line = 0; line < points.Count; line++)
        {
            for (int item = 0; item < points[0].Count; item++)
            {
                points[line][item] = points[line][item]==10?0:points[line][item] ;
            }
        }
        if (toprint)
		{
			Console.WriteLine($"After Step:");
			print2d(points,' ');
			Console.WriteLine($"Flashed this step: {flashes}");
		}
		if (flashes == 0)
			return 0;
		return flashes;
	}

	void compute()
	{
		print2d(octo);
		var totalflashes = 0;
		for (int i = 0; i < 199; i++)
		{
			var trybreak = step(octo, true, i);
			if (trybreak==-1)
			{ 
				Console.WriteLine($"-----------------------------{i+1}---------------------------------");
				break;
			}
			totalflashes += trybreak;
		}
		
        Console.WriteLine(totalflashes);
	}
	compute();
}

static void day12()
{
	//var txts = File.ReadAllLines(@"C:\Users\11349\source\repos\Advent\input12.txt");
	var test = false;
	var testinput = new List<string>();
	var testi = 2;
	testinput.Add(@"start-A
start-b
A-c
A-b
b-d
A-end
b-end");
	testinput.Add(@"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc");
	testinput.Add(@"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW");

    var txts = new StreamReader(@"C:\Users\11349\source\repos\Advent\input12.txt").ReadToEnd();
	List<List<String>> verticies = test? testinput[testi].Split('\n').Select(x => x.Trim().Split('-').ToList()).ToList(): txts.Split('\n').Select(x => x.Trim().Split('-').ToList()).ToList();
	
	void printhashset(List<List<String>> xd) => xd.ForEach(x => Console.WriteLine($"{x[0]} <--> {x[1]} "));
    
	bool isbigcave(String ww) => ww == ww.ToUpper();

    List<String> choices(String node) => verticies.Where(x => x.Contains(node)).Where(p => p.Count() > 1).Select(o => o.ToList()[1 - o.IndexOf(node)]).ToList();
	Dictionary<string, List<string>> adjacencylist = new();
	foreach (var node in verticies.SelectMany(o => o).ToList().Distinct()){
		adjacencylist[node] = choices(node);
}
	List<List<string>> paths = new();

	int findpath(string currentnode, List<string> gvisited, List<string> gpath,bool partone = true, bool print = false, string targetnode = "end")
	{
		var path = new List<string>(gpath);
		var visited = new List<string>(gvisited);
		var twice = partone ? visited.Distinct().Count() == visited.Count(): visited.Distinct().Count() != visited.Count();
		var options = new List<string>();
		options = adjacencylist[currentnode].Where(o => o != "start" && visited.FindAll(p => p == o).Count() <= 1).ToList();
		if (twice)
			options = adjacencylist[currentnode].Where(o => o != "start" && !visited.Contains(o)).ToList();
        
        if (print)
        {
            Console.Write($"-------------Options:");
            printanything(options); 
        }
		int returnval = 0;

		if (options.Count == 0)
		{
			if(print)
			Console.WriteLine($"No options");
			path.Remove(currentnode);
			visited.Remove(currentnode);
				if (visited.Contains(currentnode))
                twice = false;
			return returnval;
		}

		if (options.Any(o=>o==targetnode))
        {
			options.Remove(targetnode);
			var newpath = new List<string>(path);
			newpath.Add(targetnode);
			paths.Add(newpath);
			returnval++;
        }
        for (int i = 0; i < options.Count; i++)
        {
			path = new List<string>(gpath);
			visited = new List<string>(gvisited);
			var choice = options[i];
            if (print)
            {
                Console.WriteLine($"		--------------------------------{i}-----------------------------------------");
                Console.WriteLine($"	 Current Choice is {choice}");
                printanything(path);
                Console.Write($"		Visited Twice? {twice}. Visited nodes: ");
                printanything(visited); 
            }
            if (path.Contains(choice) && visited.Contains(choice))
                twice = true;
            if (!isbigcave(choice))
				visited.Add(choice);
			path.Add(choice);
            if (print)
            {
                Console.WriteLine($"	{choice} added to path");
                printanything(path); 
            }
				var evalchoice = findpath(choice,new List<string>(visited),new List<string>(path),partone);
            if (evalchoice == 0)
            {
				path.Remove(choice);
				visited.Remove(choice);
				//if (visited.Contains(choice))
				//	twice = false;
			}
			returnval += evalchoice;
        }
		return returnval;
    }

	void compute()
    {
		printhashset(verticies);
		string node = "start";
		bool print = false;
		var part1sum = findpath(node, new List<String>() {node}, new List<String>() {node},true,print);
		var part2sum = findpath(node, new List<String>() {node}, new List<String>() {node},false,print);

		//var indexi = 0;
  //      foreach (var upath in paths)
  //      {
  //          indexi++; Console.WriteLine($"path {indexi} of {paths.Count}");
		//	var groupbry = upath.GroupBy(o => o, o => o, (node, visited) => new
		//	{
		//		Name = node,
		//		Key = visited.Count()
		//	}).ToList();
		//	var novisited = new List<int>(){ 0, 0 };
		//	foreach (var result in groupbry)
		//	{
		//		if (result.Key == 1)
		//		{
		//			if (!isbigcave(result.Name))
		//			{
		//				novisited[0]++;
		//			}
		//		}
		//		if (result.Key >= 2)
		//		{
		//			if (!isbigcave(result.Name))
		//			{
		//				novisited[1]++;
		//			}
		//			if (novisited[1] >= 2)
		//				break;
		//		}
		//	}
		//	if (novisited[1] == 0)
		//		part1sum++;
		//	if (novisited[1] <= 1)
		//		part2sum++;
		//}
        Console.WriteLine($"part1: {part1sum}. part2: {part2sum}");
        if (print)
        {
            foreach (var item in paths)
            {
                Console.Write($"Path: ");
                printanything(item);
            } 
        }
    }
    compute();
}

static void day13()
{
	var test = false;
	var testinput = @"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5";

	var txt = test ? testinput : new StreamReader(@"C:\Users\11349\source\repos\Advent\input13.txt").ReadToEnd();
	var txts = txt.Split("\r\n\r\n");
	var folds = txts[1].Split('\n');
	var coords = txts[0].Split('\n');
	var dots = coords.Select(x=>(int.Parse(x.Split(',')[0]), int.Parse(x.Split(',')[1])).ToTuple()).ToList();

	var sizex = 2 * (folds[0].Contains("x=") ? int.Parse(folds[0].Split("=")[1]) : int.Parse(folds[1].Split("=")[1])) + 1;
	var sizey = 2 * (folds[0].Contains("y=") ? int.Parse(folds[0].Split("=")[1]) : int.Parse(folds[1].Split("=")[1])) + 1;

	//char[][] creategraph(char arrayof, int lenx,int leny)
	Func<char, int, int, char[][]> creategraph = (arrayof, lenx, leny) =>
	   {
		//var vs = new char[size][];
		//return new int[] Enumerable.Repeat<T>(arrayof, size).ToArray(); 
		char[][] flag = new char[lenx][];
		   for (int xi = 0; xi < lenx; xi++)
		   {
			   flag[xi] = new char[leny];
			   for (int yi = 0; yi < leny; yi++) flag[xi][yi] = arrayof;
		   }
		   return flag;
	   };

	void printgraph(char[][] graphtoprint, int sizex, int sizey)
	{
		for (int y1 = 0; y1 < sizey; y1++)
		{
			for (int x1 = 0; x1 < sizex; x1++)
			{
				Console.Write(graphtoprint[x1][y1]);
			}
			Console.WriteLine();
		}
	}
	void compute()
    {
		var graph = creategraph('-', sizex, sizey);
        foreach (var (dotx,doty) in dots)
        {
            //Console.WriteLine($"dot at {dotx}, {doty} of {sizex},{sizey}");
			graph[dotx][doty] = '#';
        }
		//printgraph(graph, sizex, sizey);
		var loopcount = 0;
		foreach(var fold in folds)
        {
			loopcount++;
			var foldat = fold.Contains("x=") ? 'x':'y';
			var foldvalue = int.Parse(fold.Split("=")[1]);
			//var newgraph = foldat == 'x' ? new char[foldvalue + 1][] : new char[sizex][];
			if (foldat =='x')
            {
				var newsize = new { X = foldvalue , Y = sizey };
				var newgraph = new char[newsize.X][];
				for (int i = 0; i < newsize.X; i++)
                {
					//graph[i].CopyTo(newgraph[i],0);
					newgraph[i] = graph[i].Select(a => a).ToArray();
					for (int j = 0; j < newsize.Y; j++)
                    {
                        //Console.WriteLine($"At{i},{j} with {(sizex-1)-i},{j} folded on. firstchar = {graph[i][j]} secondchar = {graph[(sizex-1)-i][j]}");
						newgraph[i][j] = graph[i][j] == graph[(sizex - 1) - i][j] ? graph[i][j] : '#';
                    }
                }
				sizey = newsize.Y;
				sizex = newsize.X;
                Console.WriteLine($"Print new graph");
				//printgraph(newgraph,newsize.X,newsize.Y);
				graph = newgraph;
            }
			if (foldat =='y')
            {
				var newsize = new { Y = foldvalue , X = sizex };
				var newgraph = new char[newsize.X][];
				for (int i = 0; i < newsize.X; i++)
                {
					newgraph[i] = new char[newsize.Y];
					Array.Copy(graph[i],newgraph[i],newsize.Y);
					for (int j = 0; j < newsize.Y; j++)
					{
						newgraph[i][j] = graph[i][j] == graph[i][(sizey - 1) - j] ? graph[i][j] :'#';
					}
				}
				sizey = newsize.Y;
				sizex = newsize.X;
				Console.WriteLine($"Print new graph");
				//printgraph(newgraph,newsize.X,newsize.Y);
				graph = newgraph;
            }
			//if (loopcount==1 && part1)
			//break;
		}
		printgraph(graph,sizex,sizey);

		var part1sum = 0;
		for (int y1 = 0; y1 < sizey; y1++)
		{
			for (int x1 = 0; x1 < sizex; x1++)
			{
				part1sum += graph[x1][y1]=='#'?1:0;
			}
		}
        Console.WriteLine($"Part-1: {part1sum}");
	}
	compute();
}

static void day14()
{
	var testinput = @"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C";

	var test = false;
	var txt = test ? testinput : new StreamReader(@"C:\Users\11349\source\repos\Advent\input14.txt").ReadToEnd();
	var txts = txt.Split("\r\n\r\n");
	var inserts = txts[1].Split('\n');
	var polymer = txts[0];
	var rules = inserts.ToDictionary(x => x.Split("->")[0].Trim(), x => x.Split("->")[1].Trim());
	var paircounts = new Dictionary<string, long>();
	var charcounts = new Dictionary<char, long>();
	bool print = false;
	//var insert1 = rules.Select(x=> new { A = x.Split("->")[0].Trim(), B = x.Split("->")[1].Trim() });
	foreach (var item in rules)
    {
		paircounts[item.Key] = (long)0;
		charcounts[item.Key[0]] = (long)0;
		charcounts[item.Key[1]] = (long)0;
		if(print)
		Console.WriteLine($"{item.Key} -> {item.Value} goes to {item.Key[0]}{item.Value}{item.Key[1]}");

	}

	void step()
    {   //StringBuilder newpolymer = new();
		var incrementby = new Dictionary<string, long>(paircounts);
		foreach (var item in paircounts)
        {
			if (item.Value > 0)
            {
				incrementby[item.Key] = paircounts[item.Key];
				paircounts[item.Key]-= incrementby[item.Key];
				charcounts[rules[item.Key][0]]+=incrementby[item.Key];
			}
        }
        foreach (var item in paircounts)
        {
            paircounts["" + item.Key[0] + rules[item.Key]] += incrementby[item.Key];
            paircounts["" + rules[item.Key] + item.Key[1]] += incrementby[item.Key]; 
        }
		//newpolymer.Append(str[str.Length - 1]);
		//return newpolymer.ToString();
	}

	void compute()
    {
		for (int i = 0; i < polymer.Length - 1; i++)
		{
			//newpolymer.AppendFormat($"{str[i]}{rules["" + str[i] + str[i + 1]]}");
			paircounts.TryGetValue("" + polymer[i] + polymer[i + 1], out var count);
            Console.WriteLine($"pair {polymer[i]}{polymer[i+1]} = {count}");
			paircounts["" + polymer[i] + polymer[i + 1]] = count + 1;
			Console.WriteLine($"pair {polymer[i]}{polymer[i + 1]} = {paircounts["" + polymer[i] + polymer[i + 1]]}");
			charcounts[polymer[i]]++;
		}
		charcounts[polymer[polymer.Length - 1]]++;

		for (int i = 0; i < 40; i++)
        {
        //Console.WriteLine($"Before step: {charcounts.Values.Sum()}");
            foreach (var (c,ccount) in charcounts)
            {
                //Console.WriteLine($"{c} occurs {ccount} times");
            }
            step();
			//foreach(var (ker,valye) in polymer.GroupBy(o => o).ToDictionary(o => o.Key, o => o.Count()))
   //         {
   //             Console.WriteLine($"{ker} occurs {valye}");
   //         }
		}
		//var counts = polymer.GroupBy(o => o).ToDictionary(o => o.Key, o => o.Count());
		var part1sum = charcounts.Values.Max() - charcounts.Values.Min();
        Console.WriteLine($"Part2 Solution: {part1sum}"); 
    }
	compute();
}

static void day15()
{
	var testinput = new List<string>();
	testinput.Add(@"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581");
	testinput.Add(@"19999
19111
11191");

	var test = false;
	var testindex = 0;
	var txt = test ? testinput[testindex] : new StreamReader(@"C:\Users\11349\source\repos\Advent\input15.txt").ReadToEnd();
	var risklevel = txt.Split('\n').Select(o => o.Trim().Select(x => x - '0').ToList()).ToList();
	var maxy = risklevel.Count;
	var maxx = risklevel[0].Count;
	int[][] lowestcost = new int[maxx][];
    for (int i = 0; i < maxx; i++)
    {
		lowestcost[i] = Enumerable.Repeat(int.MaxValue, maxy).ToArray();
    }
	int path(int x,int y,int cost)
	{
		var risk = risklevel[y][x];
		if (x == 0 && y == 0)
		{
			risk = 0;
		lowestcost[x][y] = 0;
			cost = 0;
		}
		if (cost+risk > lowestcost[x][y])
			return int.MaxValue;
		if (x == maxx - 1 && y == maxy - 1)
        {
			lowestcost[x][y] = cost + risk;
			return cost+risk < lowestcost[x][y]?cost+risk:lowestcost[x][y];
        }
		lowestcost[x][y] = cost+risk;
		
		if (x == maxx - 1)
			return risk + path(x, y + 1, cost + risk);
		if (y == maxy - 1)
			return risk + path(x + 1, y, cost + risk);

		var patha = path(x, y + 1, cost + risk);
		var pathb = path(x + 1, y, cost + risk);
		int returnval;
		if (patha < pathb)
		{
			returnval = patha;
			//Console.WriteLine($"from {x},{y} went to {x},{y + 1}");
        }
		else
		{ 
			returnval = pathb;
			//Console.WriteLine($"from {x},{y} went to {x+1},{y}");
        }
		return returnval + risk;
    }

	(int, int) div(int a)
	{
		return test?(a / 10, a % 10):(a/100,a%100);
	}

	var maxy5 = maxy*5;
	var maxx5 = maxx*5;
	
	int[][] risklevel5 = new int[maxx5][];
	for (int xxx = 0; xxx < maxx5; xxx++)
		{
			risklevel5[xxx] = new int[maxy5];
            for (int jj = 0; jj < maxy5; jj++)
            {
			var valueinoriginal = risklevel[div(jj).Item2][div(xxx).Item2] + div(xxx).Item1 + div(jj).Item1; 
				risklevel5[xxx][jj] = valueinoriginal > 9 ? valueinoriginal % 9: valueinoriginal;

            }	
		}

	int[][] lowestcost5 = new int[maxx5][];
	for (int i = 0; i < maxx5; i++)
	{
		lowestcost5[i] = Enumerable.Repeat(int.MaxValue,maxy5).ToArray();
	}
	lowestcost5[0][0] = 0;
	//for (int xx = 0; xx < maxx5; xx++)
	//{
	//	lowestcost5[xx] = new int[maxy5];
	//	for (int j = 0; j < maxy5; j++)
	//	{
	//		//var valueinoriginal = risklevel[div(xx).Item2][div(j).Item2] + div(xx).Item1 + div(j).Item1;
	//		if (j == 0 && xx == 0)
	//		{
	//			lowestcost5[xx][j] = 0;
	//			continue;
	//		}
	//		if (j == 0)
	//		{ 
	//			lowestcost5[xx][j] = lowestcost5[xx - 1][j]+ risklevel5[xx][j];
	//			continue;
	//		}
	//		if (xx == 0)
	//		{		
	//			lowestcost5[xx][j] = lowestcost5[xx][j - 1]+ risklevel5[xx][j];
	//			continue;
	//		}
	//		lowestcost5[xx][j] = lowestcost5[xx][j - 1] < lowestcost5[xx - 1][j] ? lowestcost5[xx][j - 1] : lowestcost5[xx - 1][j];
	//		lowestcost5[xx][j] += risklevel5[xx][j];
	//	}
	//}

	void dijkstra()
    {
		var queue = new PriorityQueue<(int,int),int>();
		queue.Enqueue((0,0), 0);
		while(queue.Count > 0)
        {
			var point = queue.Dequeue();
			if (point.Item1 == maxx5 - 1 && point.Item2 == maxy5 - 1)
				break;
			if (point.Item1 == maxx5 - 1 && point.Item2 == 0)
            {
				int costdown = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1][point.Item2 + 1];
				if (costdown < lowestcost5[point.Item1][point.Item2 + 1])
				{
					lowestcost5[point.Item1][point.Item2 + 1] = costdown;
					queue.Enqueue((point.Item1, point.Item2 + 1), costdown);
				}
				int costleft = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1 - 1][point.Item2];
				if (costleft < lowestcost5[point.Item1 - 1][point.Item2])
				{
					lowestcost5[point.Item1 - 1][point.Item2] = costleft;
					queue.Enqueue((point.Item1 - 1, point.Item2), costleft);
				}
				continue;
			}
			if (point.Item1 == 0 && point.Item2 == maxy5-1)
            {				
				int costright = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1 + 1][point.Item2];
				if (costright < lowestcost5[point.Item1 + 1][point.Item2])
				{
					lowestcost5[point.Item1 + 1][point.Item2] = costright;
					queue.Enqueue((point.Item1 + 1, point.Item2), costright);
				}
				int costup = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1][point.Item2 - 1];
				if (costup < lowestcost5[point.Item1][point.Item2 - 1])
				{
					lowestcost5[point.Item1][point.Item2 - 1] = costup;
					queue.Enqueue((point.Item1, point.Item2 - 1), costup);
				}
				continue;
			}
			if(point.Item1 == 0 && point.Item2 == 0)
            {
				int costright = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1 + 1][point.Item2];
				if (costright < lowestcost5[point.Item1 + 1][point.Item2])
				{
					lowestcost5[point.Item1 + 1][point.Item2] = costright;
					queue.Enqueue((point.Item1 + 1, point.Item2), costright);
				}
				int costdown = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1][point.Item2 + 1];
				if (costdown < lowestcost5[point.Item1][point.Item2 + 1])
				{
					lowestcost5[point.Item1][point.Item2 + 1] = costdown;
					queue.Enqueue((point.Item1, point.Item2 + 1), costdown);
				}
				continue;
            }
			if(point.Item1 == 0)
            {
				int costdown = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1][point.Item2 + 1];
                if (costdown < lowestcost5[point.Item1][point.Item2 + 1])
                {
                    lowestcost5[point.Item1][point.Item2 + 1] = costdown;
                    queue.Enqueue((point.Item1, point.Item2 + 1), costdown);
                }

                int costright = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1 + 1][point.Item2];
                if (costright < lowestcost5[point.Item1 + 1][point.Item2])
                {
                    lowestcost5[point.Item1 + 1][point.Item2] = costright;
                    queue.Enqueue((point.Item1 + 1, point.Item2), costright);
                }
                int costup = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1][point.Item2 - 1];
                if (costup < lowestcost5[point.Item1][point.Item2 - 1])
                {
                    lowestcost5[point.Item1][point.Item2 - 1] = costup;
                    queue.Enqueue((point.Item1, point.Item2 - 1), costup);
				}
				continue;
			}
			if(point.Item2 == 0)
            {
				int costdown = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1][point.Item2 + 1];
				if (costdown < lowestcost5[point.Item1][point.Item2 + 1])
				{
					lowestcost5[point.Item1][point.Item2 + 1] = costdown;
					queue.Enqueue((point.Item1, point.Item2 + 1), costdown);
				}

				int costright = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1 + 1][point.Item2];
				if (costright < lowestcost5[point.Item1 + 1][point.Item2])
				{
					lowestcost5[point.Item1 + 1][point.Item2] = costright;
					queue.Enqueue((point.Item1 + 1, point.Item2), costright);
				}
				int costleft = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1 - 1][point.Item2];
				if (costleft < lowestcost5[point.Item1 - 1][point.Item2])
				{
					lowestcost5[point.Item1 - 1][point.Item2] = costleft;
					queue.Enqueue((point.Item1 - 1, point.Item2), costleft);
				} continue;
			}
			if (point.Item1 == maxx5 - 1)
			{
				int costdown = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1][point.Item2 + 1];
				if (costdown < lowestcost5[point.Item1][point.Item2 + 1])
				{
					lowestcost5[point.Item1][point.Item2 + 1] = costdown;
					queue.Enqueue((point.Item1, point.Item2 + 1), costdown);
				}
				int costup = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1][point.Item2 - 1];
				if (costup < lowestcost5[point.Item1][point.Item2 - 1])
				{
					lowestcost5[point.Item1][point.Item2 - 1] = costdown;
					queue.Enqueue((point.Item1, point.Item2 - 1), costdown);
				}

				int costleft = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1 - 1][point.Item2];
				if (costleft < lowestcost5[point.Item1 - 1][point.Item2])
				{
					lowestcost5[point.Item1 - 1][point.Item2] = costleft;
					queue.Enqueue((point.Item1 - 1, point.Item2), costleft);
				}
				continue;
			}
			if (point.Item2 == maxy5 - 1)
			{
				int costright = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1 + 1][point.Item2];
				if (costright < lowestcost5[point.Item1 + 1][point.Item2])
				{
					lowestcost5[point.Item1 + 1][point.Item2] = costright;
					queue.Enqueue((point.Item1 + 1, point.Item2), costright);
				}
				int costup = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1][point.Item2 - 1];
				if (costup < lowestcost5[point.Item1][point.Item2 - 1])
				{
					lowestcost5[point.Item1][point.Item2 - 1] = costup;
					queue.Enqueue((point.Item1, point.Item2 - 1), costup);
				}

				int costleft = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1 - 1][point.Item2];
				if (costleft < lowestcost5[point.Item1 - 1][point.Item2])
				{
					lowestcost5[point.Item1 - 1][point.Item2] = costleft;
					queue.Enqueue((point.Item1 - 1, point.Item2), costleft);
				}
				continue;
			}

            else
            {
                int costdown = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1][point.Item2 + 1];
                if (costdown < lowestcost5[point.Item1][point.Item2 + 1])
                {
                    lowestcost5[point.Item1][point.Item2 + 1] = costdown;
                    queue.Enqueue((point.Item1, point.Item2 + 1), costdown);
                }

                int costright = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1 + 1][point.Item2];
                if (costright < lowestcost5[point.Item1 + 1][point.Item2])
                {
                    lowestcost5[point.Item1 + 1][point.Item2] = costright;
                    queue.Enqueue((point.Item1 + 1, point.Item2), costright);
                }
                int costup = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1][point.Item2 - 1];
                if (costup < lowestcost5[point.Item1][point.Item2 - 1])
                {
                    lowestcost5[point.Item1][point.Item2 - 1] = costup;
                    queue.Enqueue((point.Item1, point.Item2 - 1), costup);
                }

                int costleft = lowestcost5[point.Item1][point.Item2] + risklevel5[point.Item1 - 1][point.Item2];
                if (costleft < lowestcost5[point.Item1 - 1][point.Item2])
                {
                    lowestcost5[point.Item1 - 1][point.Item2] = costleft;
                    queue.Enqueue((point.Item1 - 1, point.Item2), costleft);
                } 
            } 
		}
    }

	void compute()
    {
		
		var sw = new System.Diagnostics.Stopwatch();
		sw.Start();
		dijkstra();
        var part2sum = lowestcost5[maxx5-1][maxy5-1];
		sw.Stop();
		//print2d(risklevel5);
        Console.WriteLine($"lowest value: {part2sum} at {maxx5-1},{maxy5-1} and lastriskvalue = {risklevel5[maxx5-1][maxy5-1]}");
		Console.WriteLine($"Elapsed; {sw.ElapsedMilliseconds}ms");
	}
	compute();
}

static void day16()
{
    string setup(bool test = true,int testindex = 0)
    {
		var testinput = new List<string>();
		testinput.Add(@"D2FE28");
		testinput.Add(@"34006F45291200");
		testinput.Add(@"EE00D40C823060");
		testinput.Add(@"8A004A801A8002F478");
		testinput.Add(@"620080001611562C8802118E34");
		testinput.Add(@"C0015000016115A2E0802F182340");
		testinput.Add(@"A0016C880162017C3686B18A3D4780");
        //var txts = File.ReadAllLines(@"C:\Users\11349\source\repos\Advent\input11.txt");
        var txt = test ? testinput[testindex] : new StreamReader(@"C:\Users\11349\source\repos\Advent\input16.txt").ReadToEnd();
		Dictionary<char, string> hex = new();
		hex['0'] = "0000";
		hex['1'] = "0001";
		hex['2'] = "0010";
		hex['3'] = "0011";
		hex['4'] = "0100";
		hex['5'] = "0101";
		hex['6'] = "0110";
		hex['7'] = "0111";
		hex['8'] = "1000";
		hex['9'] = "1001";
		hex['A'] = "1010";
		hex['B'] = "1011";
		hex['C'] = "1100";
		hex['D'] = "1101";
		hex['E'] = "1110";
		hex['F'] = "1111";
		return string.Join("",txt.Select(o=>hex[o]));
    }
	var index = 0;
	long part1 = 0;
	Dictionary<string,long> literalparse(string packet)
    {
                        Console.WriteLine($"index: {index} ");
		//dynamic literalparse(int index,string packet)
		int version = Convert.ToInt32(packet.Substring(index, 3),2);
		index += 3;
		int type = Convert.ToInt32(packet.Substring(index, 3), 2);
		index += 3;
		if (type == 4)
        {
			var groups = new StringBuilder();
			bool lastgroup = false;
			while(!lastgroup)
            {
				if (packet[index] == '0')
					lastgroup = true;
				index++;
				groups.Append(packet.Substring(index, 4));
				index += 4;
            }
			
			var literal = Convert.ToInt64(groups.ToString(),2);
			//var literal = 0;
			index = packet.Length - index < 11 ? -1 :index;
			index = packet.Length < index ? -1 : index;
			//index += (1+index) % 4;
			return new Dictionary<string, long>() { { "index", index }, { "version", version }, { "type", type }, { "literal", literal }, { "literalvalue", literal } };
			//return new { index = index, version=version, type=type, literal=literal };
        }
		if (type != 4)
        {
			var lengthtype = 0;
			var lengthvalue = 0;
			if(packet[index] == '0')
				lengthtype = 15;
			if(packet[index] == '1')
				lengthtype = 11;
			index++;
			lengthvalue = Convert.ToInt32(packet.Substring(index, lengthtype), 2);
			index += lengthtype;
			index = packet.Length - index < 11 ? -1 : index;
			index = packet.Length < index ? -1 : index;
			return new Dictionary<string, long>() { { "index",  index }, { "version",  version }, { "type",  type }, { "lengthtype",  lengthtype }, { "lengthvalue",  lengthvalue } };
			//return new { index=index, version=version, type=type, lengthtype=lengthtype, lengthvalue=lengthvalue };
        }
		return new Dictionary<string, long>();
    }

	Dictionary<string, long> loop(string packets)
    {
		var parsed = literalparse(packets);
		part1 += parsed["version"];
                        Console.WriteLine($"start adding {parsed["version"]}");
		while (index != -1)
		{
            Console.WriteLine($"loop");
			if (parsed.ContainsKey("lengthtype"))
            {
				var lengthvalue = parsed["lengthvalue"];
				var accum = new List<long>();
				long action=-1;
                if (parsed["lengthtype"] == 11)
                {
						action = parsed["type"];
                    for (int i = 0; i < lengthvalue; i++)
                    {
						if (index == -1)
							break;
                        var parsed1 = loop(packets);
						index = packets.Length - index < 11 ? -1 : index;
						index = packets.Length < index ? -1 : index;
                        //part1 += parsed["version"];
                        //if (action != 4 && index != -1)
                        //                  {
                        //	parsed = loop(packets);
                        //                  }
                        Console.WriteLine("adding values to accum");
						accum.Add(parsed1["literalvalue"]);
					}
					if (action == 0 )
                    {
						parsed["literalvalue"] = accum.Sum();
						return parsed;
                    }
					if (action == 1)
					{
						parsed["literalvalue"] = accum.Aggregate((a,b)=>a*b);
						return parsed;
					}
					if (action == 2)
					{
						parsed["literalvalue"] = accum.Min();
						return parsed;
					}
					if (action == 3)
					{
						parsed["literalvalue"] = accum.Max();
						return parsed;
					}
					if (action == 5)
					{
						parsed["literalvalue"] = accum[0] > accum[1] ? 1 : 0;
						return parsed;
					}
					if (action == 6)
					{
						parsed["literalvalue"] = accum[0] < accum[1] ? 1 : 0;
						return parsed;
					}
					if (action == 7)
					{
						parsed["literalvalue"] = accum[0] == accum[1] ? 1 : 0;
						return parsed;
					}
				}
                else if(parsed["lengthtype"] == 15)
                {
						action = parsed["type"];
                    while (lengthvalue!=0 && index != 1)
                    {
                        var startindex = index;
                        var parsed2 = loop(packets);
						index = packets.Length < index ? -1 : index;
						//part1 += parsed["version"];
						var endindex = index==-1?packets.Length-1:index;
						var deltaindex = endindex - startindex;
						lengthvalue -= deltaindex;
                        Console.WriteLine("adding values to accum");
						accum.Add(parsed2["literalvalue"]);
						if (deltaindex == 0 || endindex == packets.Length - 1)
							break;
						//if (action != 4 && index != -1 )
						//{
						//	parsed = loop(packets);
						//}
					}
					if (action == 0)
					{
						parsed["literalvalue"] = accum.Sum();
						return parsed;
					}
					if (action == 1)
					{
						parsed["literalvalue"] = accum.Aggregate((a, b) => a * b);
						return parsed;
					}
					if (action == 2)
					{
						parsed["literalvalue"] = accum.Min();
						return parsed;
					}
					if (action == 3)
					{
						parsed["literalvalue"] = accum.Max();
						return parsed;
					}
					if (action == 5)
					{
						parsed["literalvalue"] = accum[0] > accum[1] ? 1 : 0;
						return parsed;
					}
					if (action == 6)
					{
						parsed["literalvalue"] = accum[0] < accum[1] ? 1 : 0;
						return parsed;
					}
					if (action == 7)
					{
						parsed["literalvalue"] = accum[0] == accum[1] ? 1 : 0;
						return parsed;
					}
				}
				if (packets.Length - index < 11)
					return parsed;
				//if (index!=-1)	
				//parsed = loop(packets);
            }
			else if (parsed.ContainsKey("literal"))
            {
				//if (packets.Length - index < 11)
					return parsed;
				//if (index == -1)
				//	return parsed;
				//parsed = loop(packets);
			}
		}
		return parsed;
	}

    void compute()
	{
		var sw = new System.Diagnostics.Stopwatch();
		sw.Start();
		var packets = setup(true,1);
        Console.WriteLine(packets);
		var output = loop(packets);
        //var output = literalparse(packets);
		sw.Stop();
        if(output.ContainsKey("lengthtype"))
		Console.WriteLine($"version: {output["version"]} packet type: {output["type"]}. length type: {output["lengthtype"]}. length value: {output["lengthvalue"]} literalvalue: {output["literalvalue"]} index at end: {index} part1: {part1}");
		if (output.ContainsKey("literal"))
        Console.WriteLine($"version: {output["version"]} packet type: {output["type"]}. literal: {output["literal"]} index at end: {index} part1: {part1}");
		Console.WriteLine($"Elapsed; {sw.ElapsedMilliseconds}ms");
	}
    compute();
}

static void day17()
{
    ((int,int),(int,int)) setup(bool test = true)
    {
        var testinput = @"target area: x=20..30, y=-10..-5";
        var txt = test ? testinput : new StreamReader(@"C:\Users\11349\source\repos\Advent\input17.txt").ReadToEnd();
		var targetx = (int.Parse(txt.Trim().Split(":")[1].Split(",")[0].Split("=")[1].Split("..")[0]), int.Parse(txt.Trim().Split(":")[1].Split(",")[0].Split("=")[1].Split("..")[1]));
		var targety = (int.Parse(txt.Trim().Split(":")[1].Split(",")[1].Split("=")[1].Split("..")[0]), int.Parse(txt.Trim().Split(":")[1].Split(",")[1].Split("=")[1].Split("..")[1]));
		return new ( targetx,targety );
	}

	((int,int),(int,int)) step((int ,int ) velocity, (int, int) pos)
    {
		var (x, y) = velocity;
		var (xpos, ypos) = pos;
		xpos += x;
		ypos += y;
		y--;
		if (x != 0)
			x += x < 0 ? 1 : -1;
		return ((x, y), (xpos, ypos));
    }

	(bool,bool) attarget((int, int) pos, ((int, int), (int, int)) target)
    {
		return (target.Item1.Item1 <= pos.Item1 && pos.Item1 <= target.Item1.Item2,  target.Item2.Item2 >= pos.Item2 && pos.Item2 >= target.Item2.Item1);
    }

	(bool,int) checkpath((int, int) vel, (int, int) pos,((int, int), (int, int)) target)
    {
		var maxy = pos.Item2;
		while (true)
		{
			var at = attarget(pos, target);
			if (at.Item1 && at.Item2)
				return (true,maxy);
			if (!at.Item1 && vel.Item1 == 0)
			{
                //Console.WriteLine($"stationary"); 
				return (false,0);
		}
            if (vel.Item2 < 0 && pos.Item2 < target.Item2.Item2)
			{
				//Console.WriteLine($"below target. Vel {vel.Item2}. pos {pos.Item2}. target {target.Item2.Item2}");
				return (false,0);
			}
			if (vel.Item1 >= 0 && pos.Item1 > target.Item1.Item2)
			{
				//Console.WriteLine($"overshot right");
				return (false,0);
			}
			if (vel.Item1 <= 0 && pos.Item1 < target.Item1.Item1)
			{
				//Console.WriteLine($"overshot left");
				return (false,0);
			}
			(vel, pos) = step(vel, pos); 
			maxy = maxy > pos.Item2 ? maxy : pos.Item2;
        }
		return (true,maxy);
    }

		var list = new List<(int, int)>();
	int tryy(((int, int), (int, int)) target)
	{
		var maxy = 0;
		for (int i = 1; i < target.Item1.Item2+1; i++)
		{
			for (int j = -2000; j < 5000; j++)
			{
				var neww = checkpath((i, j), (0, 0), target);
				if (!neww.Item1)
					continue;
				if (neww.Item1)
				{ maxy = maxy > neww.Item2 ? maxy : neww.Item2;
					list.Add((i, j));
				}
				
			}
		}
		return maxy;
	}

	void compute()
    {
		var test = false;
		var target = setup(test);
		var sw = new System.Diagnostics.Stopwatch();
		sw.Start();
      Console.WriteLine($"X from {target.Item1.Item1} to {target.Item1.Item2}. Y from {target.Item2.Item1} to {target.Item2.Item2}");
		var pos = (0, 0);
		var vel = (6, 9);
		var result = checkpath(vel, pos, target);
		Console.WriteLine($"starting velocit {vel.Item1},{vel.Item2} is {result.Item1} highest {result.Item2}");
        Console.WriteLine($"higest reached: {tryy(target)}. Unique trajectories: {list.Count}");
		sw.Stop();
		Console.WriteLine($"Elapsed; {sw.ElapsedMilliseconds}ms");
	}
	compute();
}

static void day18()
{
	List<string> setup(bool test = true, int testindex = 0)
	{
		var testinput = new List<string>();
		testinput.Add(@"[1,1]
[2,2]
[3,3]
[4,4]"); // [[[[1,1],[2,2]],[3,3]],[4,4]]

		testinput.Add(@"[1,1]
[2,2]
[3,3]
[4,4]
[5,5]"); // [[[[3,0],[5,3]],[4,4]],[5,5]]

		testinput.Add(@"[1,1]
[2,2]
[3,3]
[4,4]
[5,5]
[6,6]"); // [[[[5,0],[7,4]],[5,5]],[6,6]]

		testinput.Add(@"[[[[4,3],4],4],[7,[[8,4],9]]]
[1,1]"); // [[[[0,7],4],[[7,8],[6,0]]],[8,1]]

		testinput.Add(@"[[[0,[4,5]],[0,0]],[[[4,5],[2,6]],[9,5]]]
[7,[[[3,7],[4,3]],[[6,3],[8,8]]]]
[[2,[[0,8],[3,4]]],[[[6,7],1],[7,[1,6]]]]
[[[[2,4],7],[6,[0,5]]],[[[6,8],[2,8]],[[2,1],[4,5]]]]
[7,[5,[[3,8],[1,4]]]]
[[2,[2,2]],[8,[8,1]]]
[2,9]
[1,[[[9,3],9],[[9,0],[0,7]]]]
[[[5,[7,4]],7],1]
[[[[4,2],2],6],[8,7]]"); // [[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]

		testinput.Add(@"[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]
[[[5,[2,8]],4],[5,[[9,9],0]]]
[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]
[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]
[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]
[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]
[[[[5,4],[7,7]],8],[[8,3],8]]
[[9,3],[[9,9],[6,[4,9]]]]
[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]
[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]"); //[[[[6,6],[7,6]],[[7,7],[7,0]]],[[[7,7],[7,7]],[[7,8],[9,9]]]] becomes 4140

		var txt = test ? testinput[testindex] : new StreamReader(@"C:\Users\11349\source\repos\Advent\input18.txt").ReadToEnd();
		return txt.Split('\n').Select(o => o.Replace("\n", "").Trim()).ToList();
	}
	var digits = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

	(int, int) pair(string s, int index, int len)
	{
		var parse = System.Text.Json.JsonSerializer.Deserialize<int[]>(s.Substring(index, len));
		Console.WriteLine($"{parse[0]}");
		return (parse[0], parse[1]);
	}

	bool twodigit(int i, string s)
	{
		return digits.Contains(s[i]) && digits.Contains(s[i + 1]);
	}

	string addstring(string s, string t)
	{
		Console.WriteLine();
		var returnval = "[" + s + "," + t + "]".Replace(" ", "");
		//Console.WriteLine($"adding {s} and {t}");
		if (s == "")
			returnval = t;
		if (t == "")
			returnval = s;
		return returnval;
	}

	string explodepair(int index, string s)
	{
		int pairlength = 5;
		int secondat = 3;
		bool twoleft = twodigit(index + 1, s);
		bool tworight = twodigit(index + 3, s);
		if (twoleft)
		{
			pairlength = 6;
			secondat = 4;
			tworight = twodigit(index + 4, s);
		}
		if (!twoleft && tworight)
			pairlength = 6;
		pairlength = tworight && twoleft ? 7 : pairlength;
		var pairtoremove = s.Substring(index, pairlength);
		//Console.WriteLine($"pair is: {pairtoremove} from {s}");
		s = s.Remove(index, pairlength).Insert(index, "0");
		if (index > 4 && s.Length - (index + 1) >= 5)
		{
			int right = -1;
			if (!tworight)
				right = int.Parse(pairtoremove.Substring(secondat, 1));
			if (tworight)
				right = int.Parse(pairtoremove.Substring(secondat, 2));
			int firstright;
			for (int j = index + 1; j < s.Length - 1; j++)
			{
				if (digits.Contains(s[j]))
				{
					if (digits.Contains(s[j + 1]))
					{
						firstright = int.Parse(s.Substring(j, 2)) + right;
						s = s.Remove(j, 2).Insert(j, firstright.ToString());
						break;
					}
					firstright = int.Parse(s.Substring(j, 1)) + right;
					s = s.Remove(j, 1).Insert(j, firstright.ToString());
					break;
				}
			}
			int left = -1;
			if (!twoleft)
				left = int.Parse(pairtoremove.Substring(1, 1));
			if (twoleft)
				left = int.Parse(pairtoremove.Substring(1, 2));
			int firstleft;
			for (int i = index - 1; i > 0; i--)
			{
				if (digits.Contains(s[i]))
				{
					if (digits.Contains(s[i - 1]))
					{
						firstleft = int.Parse(s.Substring(i - 1, 2)) + left;
						s = s.Remove(i - 1, 2).Insert(i - 1, firstleft.ToString());
						if (firstleft < 10)
							index--;
						break;
					}
					firstleft = int.Parse(s.Substring(i, 1)) + left;
					s = s.Remove(i, 1).Insert(i, firstleft.ToString());
					if (firstleft > 9)
						index++;
					break;
				}
			}

		}
		if (index < 5)
		{
			int right = -1;
			if (!tworight)
				right = int.Parse(pairtoremove.Substring(secondat, 1));
			if (tworight)
				right = int.Parse(pairtoremove.Substring(secondat, 2));
			int firstright;
			for (int j = index + 1; j < s.Length - 1; j++)
			{
				if (digits.Contains(s[j]))
				{
					if (digits.Contains(s[j + 1]))
					{
						firstright = int.Parse(s.Substring(j, 2)) + right;
						s = s.Remove(j, 2).Insert(j, firstright.ToString());
						break;
					}
					firstright = int.Parse(s.Substring(j, 1)) + right;
					s = s.Remove(j, 1).Insert(j, firstright.ToString());
					break;
				}
			}
		}
		if (s.Length - (index + 1) < 5)
		{
			int left = -1;
			if (!twoleft)
				left = int.Parse(pairtoremove.Substring(1, 1));
			if (twoleft)
				left = int.Parse(pairtoremove.Substring(1, 2));
			int firstleft;
			for (int i = index - 1; i > 0; i--)
			{
				if (digits.Contains(s[i]))
				{
					if (digits.Contains(s[i - 1]))
					{
						firstleft = int.Parse(s.Substring(i - 1, 2)) + left;
						s = s.Remove(i - 1, 2).Insert(i - 1, firstleft.ToString());
						if (firstleft < 10)
							index--;
						break;
					}
					firstleft = int.Parse(s.Substring(i, 1)) + left;
					s = s.Remove(i, 1).Insert(i, firstleft.ToString());
					if (firstleft > 9)
						index++;
					break;
				}
			}
		}
		return s;
	}

	string findexplode(string s)
	{
		var index = 0;
		var depth = 0;
		for (int i = 0; i < s.Length; i++, index++)
		{
			depth += s[index] == '[' ? 1 : (s[index] == ']' ? -1 : 0);
			if (digits.Contains(s[index]) && depth >= 5 && ((s[index + 1] == ',' && digits.Contains(s[index + 2])) || (digits.Contains(s[index + 1]) && s[index + 2] == ',' && digits.Contains(s[index + 3]))))
			{
				return explodepair(--index, s);
			}
		}
		return s;
	}

	string split(int index, string s)
	{
		int numbertosplit = int.Parse(s.Substring(index, 2));
		//Console.WriteLine($"Splitting: {numbertosplit} from {s}");
		s = s.Remove(index, 2).Insert(index, $"[{numbertosplit / 2},{numbertosplit / 2 + numbertosplit % 2}]");
		return s;
	}

	string findsplit(string s)
	{
		var index = 0;
		for (int i = 0; i < s.Length; i++, index++)
		{
			if (digits.Contains(s[index]) && digits.Contains(s[index + 1]))
			{
				return split(index, s);
			}
		}
		return s;
	}

	string reduce(string s)
	{
		var changed = true;
		while (changed)
		{
			var before = s;
			bool nomoreexplosions = true;
			while (nomoreexplosions)
			{
				var beforeexposion = s;
				s = findexplode(s);
				var afterexplosion = s;
				nomoreexplosions = beforeexposion != afterexplosion;
			}
			s = findsplit(s);
			var after = s;
			changed = before != after;
		}
		return s;
	}

	int findmagnitude(string s)
    {
		if(s.Length == 1)
        {
			return int.Parse(s);
        }
		if(s.Length == 5)
        {
			var array = System.Text.Json.JsonSerializer.Deserialize<int[]>(s);
			return array[0] * 3 + array[1] * 2;
        }
		int depth = 0;
		for (int index = 0; index < s.Length; index++)
		{
			depth += s[index] == '[' ? 1 : (s[index] == ']' ? -1 : 0);
			if (s[index] == ',' && depth == 1)
			{
				s = s.Remove(s.Length - 1, 1).Remove(0, 1);
				index--;
				int left = findmagnitude(s.Substring(0, index));
				int right = findmagnitude(s.Substring(index + 1, s.Length - index - 1));
				return left*3 + right*2;
			}
		}
		return 0;
	}

    void compute()
	{
		bool test = false;
		int testindex = 5;
		var sw = new System.Diagnostics.Stopwatch();
		sw.Start();
		var numbers = setup(test, testindex);
		//var upto = 10;
		//numbers = numbers.Take(upto).ToList();
        //var output = numbers.Take(upto).ToList().Aggregate((a, c) => reduce(addstring(a, c)));
        //Console.WriteLine($"{output} {(output == correctoutput[upto]?"does":"does not")} equal {correctoutput[upto]}");
		var correctoutput = new List<string>();
		{	correctoutput.Add("hi");
		correctoutput.Add("hello");
		correctoutput.Add("[[[[4,0],[5,4]],[[7,7],[6,0]]],[[8,[7,7]],[[7,9],[5,0]]]]");
		correctoutput.Add("[[[[6,7],[6,7]],[[7,7],[0,7]]],[[[8,7],[7,7]],[[8,8],[8,0]]]]");
		correctoutput.Add("[[[[7,0],[7,7]],[[7,7],[7,8]]],[[[7,7],[8,8]],[[7,7],[8,7]]]]");
		correctoutput.Add("[[[[7,7],[7,8]],[[9,5],[8,7]]],[[[6,8],[0,8]],[[9,9],[9,0]]]]");
		correctoutput.Add("[[[[6,6],[6,6]],[[6,0],[6,7]]],[[[7,7],[8,9]],[8,[8,1]]]]");
		correctoutput.Add("[[[[6,6],[7,7]],[[0,7],[7,7]]],[[[5,5],[5,6]],9]]");
		correctoutput.Add("[[[[7,8],[6,7]],[[6,8],[0,8]]],[[[7,7],[5,0]],[[5,5],[5,6]]]]");
		correctoutput.Add("[[[[7,7],[7,7]],[[8,7],[8,7]]],[[[7,0],[7,7]],9]]");
		correctoutput.Add("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]");
	}
		int max = 0;
		for(int i = 0; i < numbers.Count; i++)
        {
            for (int j = 0; j < numbers.Count; j++)
            {
				if (i == j)
					continue;
				max = Math.Max(Math.Max(max, findmagnitude(reduce(addstring(numbers[i], numbers[j])))), findmagnitude(reduce(addstring(numbers[j], numbers[i]))));
            }
        }
		var output = numbers.Aggregate((a, c) => reduce(addstring(a, c)));
        Console.WriteLine($"Part 1 {output} and {findmagnitude(output)}");
        Console.WriteLine($"Part 2: {max}");
        sw.Stop();
        Console.WriteLine($"Elapsed; {sw.ElapsedMilliseconds}ms");
    }
	compute();
}

static void day19()
{
    Dictionary<int,List<string>> setup(bool test = true,int testindex=0)
    {
		var testinput = new List<string>();
			testinput.Add(@"--- scanner 0 ---
0,2
4,1
3,3

--- scanner 1 ---
-1,-1
-5,0
-2,1");
		testinput.Add(@"--- scanner 0 ---
404,-588,-901
528,-643,409
-838,591,734
390,-675,-793
-537,-823,-458
-485,-357,347
-345,-311,381
-661,-816,-575
-876,649,763
-618,-824,-621
553,345,-567
474,580,667
-447,-329,318
-584,868,-557
544,-627,-890
564,392,-477
455,729,728
-892,524,684
-689,845,-530
423,-701,434
7,-33,-71
630,319,-379
443,580,662
-789,900,-551
459,-707,401

--- scanner 1 ---
686,422,578
605,423,415
515,917,-361
-336,658,858
95,138,22
-476,619,847
-340,-569,-846
567,-361,727
-460,603,-452
669,-402,600
729,430,532
-500,-761,534
-322,571,750
-466,-666,-811
-429,-592,574
-355,545,-477
703,-491,-529
-328,-685,520
413,935,-424
-391,539,-444
586,-435,557
-364,-763,-893
807,-499,-711
755,-354,-619
553,889,-390

--- scanner 2 ---
649,640,665
682,-795,504
-784,533,-524
-644,584,-595
-588,-843,648
-30,6,44
-674,560,763
500,723,-460
609,671,-379
-555,-800,653
-675,-892,-343
697,-426,-610
578,704,681
493,664,-388
-671,-858,530
-667,343,800
571,-461,-707
-138,-166,112
-889,563,-600
646,-828,498
640,759,510
-630,509,768
-681,-892,-333
673,-379,-804
-742,-814,-386
577,-820,562

--- scanner 3 ---
-589,542,597
605,-692,669
-500,565,-823
-660,373,557
-458,-679,-417
-488,449,543
-626,468,-788
338,-750,-386
528,-832,-391
562,-778,733
-938,-730,414
543,643,-506
-524,371,-870
407,773,750
-104,29,83
378,-903,-323
-778,-728,485
426,699,580
-438,-605,-362
-469,-447,-387
509,732,623
647,635,-688
-868,-804,481
614,-800,639
595,780,-596

--- scanner 4 ---
727,592,562
-293,-554,779
441,611,-461
-714,465,-776
-743,427,-804
-660,-479,-426
832,-632,460
927,-485,-438
408,393,-506
466,436,-512
110,16,151
-258,-428,682
-393,719,612
-211,-452,876
808,-476,-593
-575,615,604
-485,667,467
-680,325,-822
-627,-443,-432
872,-547,-609
833,512,582
807,604,487
839,-516,451
891,-625,532
-652,-548,-490
30,-46,-14");
		testinput.Add(@"--- scanner 0 ---
-817,-765,856
443,-709,-511
-658,753,-745
378,506,-625
557,-593,616
-622,-827,819
-611,-838,856
-433,650,563
-586,-856,-622
398,565,499
229,541,474
585,-710,-578
-584,611,490
-796,-861,-671
528,-778,-656
-448,738,509
702,-600,648
-635,590,-725
368,455,500
339,605,-490
288,624,-682
-687,-819,-750
-646,726,-814
-134,-69,143
-4,-120,3
632,-644,504

--- scanner 1 ---
-576,-655,-870
83,71,-65
455,510,-438
-496,-588,-822
-601,-396,364
-752,-444,373
601,-737,495
125,-92,-181
402,514,256
505,551,-412
-407,683,546
-700,501,-622
603,-857,372
-717,-310,415
-409,-628,-813
545,-770,-395
363,354,318
-538,654,501
395,344,337
466,407,-474
-784,586,-567
634,-809,-409
687,-686,-444
606,-670,479
-593,685,436
-752,662,-653

--- scanner 2 ---
-351,-447,608
-522,-602,-725
652,-529,515
839,-608,-778
116,-62,-109
606,504,639
-288,499,737
-219,487,710
-283,528,-835
-455,-744,-726
612,637,-413
646,-520,584
-408,-537,490
-543,-498,589
-411,427,-872
530,514,796
805,-675,-742
-308,476,574
872,621,-400
-362,637,-877
496,468,745
740,489,-416
964,-658,-827
-377,-687,-794
471,-547,543

--- scanner 3 ---
-600,-676,-662
690,-716,598
317,-749,-574
716,563,-791
444,358,756
523,-729,495
47,106,1
-456,-623,-710
-634,789,-520
-625,659,492
-649,745,633
-514,796,-415
496,487,728
340,-665,-745
-548,-726,-667
762,490,-879
-64,40,-123
-576,-661,725
867,555,-818
346,499,705
559,-583,601
305,-766,-626
-584,685,482
-652,-454,737
-455,845,-563
-612,-636,677

--- scanner 4 ---
555,657,454
-681,394,490
-492,305,-497
-707,-820,611
720,-949,377
818,447,-591
-631,473,537
770,492,-727
193,7,-89
-246,-812,-593
-466,321,-586
696,-780,402
637,-890,-779
-659,-811,762
-793,-769,727
467,577,391
755,-800,-722
-366,-689,-591
774,355,-587
656,-839,-587
-433,-705,-596
806,-810,403
417,625,515
109,-153,6
-671,504,536
-457,253,-428

--- scanner 5 ---
492,549,-889
676,-597,352
-552,-915,660
685,-637,439
-521,633,-789
-2,-104,3
-431,515,-846
-102,40,-105
515,386,-909
503,413,-978
290,489,399
733,-570,519
-541,574,335
441,528,326
-511,684,336
272,-416,-691
305,401,345
-501,-829,544
307,-539,-744
258,-455,-602
-482,534,-654
-510,-781,695
-632,-488,-658
-616,-555,-663
-509,-368,-660
-372,598,310

--- scanner 6 ---
514,-446,-382
414,762,-372
-307,640,368
-763,741,-861
-499,-612,393
415,-376,-468
-304,643,449
-518,-643,425
381,743,-590
795,-343,698
-592,-701,-409
-824,855,-903
838,-421,652
-429,629,495
430,-425,-453
858,-439,598
584,691,680
448,619,744
-608,-645,-578
-416,-656,379
52,-36,-100
553,722,676
-37,110,83
-842,751,-763
371,655,-417
-529,-773,-524

--- scanner 7 ---
-687,452,941
-356,789,-401
811,-554,-481
897,-543,-349
594,-328,503
624,844,676
-580,503,809
638,-375,558
-25,138,155
832,839,-318
748,890,-467
-609,402,922
-430,859,-504
802,-497,-426
-521,-476,669
-360,-391,667
-604,-522,-809
-505,-560,-728
-381,795,-329
98,-36,82
590,706,799
-740,-513,-735
-434,-514,604
608,919,748
861,804,-541
689,-335,673

--- scanner 8 ---
-396,443,652
-446,501,582
883,-477,-273
893,965,-608
823,-389,-359
938,922,-715
42,-10,-17
-669,-725,483
-701,-787,591
-373,-699,-453
-744,549,-479
928,822,493
-472,490,571
688,-496,-328
618,-742,726
152,149,23
-556,-654,-489
684,-778,544
-650,-739,456
720,955,-696
-392,-555,-479
674,-732,650
842,921,423
-665,709,-497
-676,599,-652
857,906,472

--- scanner 9 ---
-74,11,-72
-84,-133,86
668,503,-564
539,815,778
522,656,693
-667,720,-632
453,-604,677
677,497,-633
-711,-836,795
-493,463,605
-581,710,-619
-806,-655,798
699,-692,-638
-495,-692,-520
750,-500,-657
589,690,729
510,-526,745
650,322,-592
-671,712,-682
-488,524,641
595,-671,676
660,-557,-539
-587,-688,-476
-455,323,626
-632,-552,-524
-695,-710,809

--- scanner 10 ---
-414,391,538
-875,-448,-650
373,308,473
-949,-584,-641
654,-692,573
-426,393,596
529,-667,-687
-446,273,587
527,-828,-772
-79,-99,-74
640,-697,443
5,43,50
709,558,-396
505,-743,531
-625,-713,705
-830,575,-716
434,346,377
-636,-849,854
-536,-746,797
445,383,382
-830,494,-503
-875,519,-578
464,-754,-649
-868,-683,-603
748,531,-351
801,499,-520

--- scanner 11 ---
421,-382,-811
454,-471,-719
-823,-412,652
413,618,635
-615,517,821
475,808,-587
-659,572,774
-599,-532,-347
583,-795,627
500,683,673
-918,-335,606
484,-714,589
-832,-467,595
424,659,-545
-710,687,814
-641,664,-871
-583,665,-782
-535,632,-812
719,-721,636
318,-482,-868
-461,-512,-436
9,143,51
470,794,-457
-122,11,-24
-384,-464,-361
339,681,758

--- scanner 12 ---
677,-807,-475
-514,-719,780
-723,-601,-559
840,468,-281
-721,-664,-469
682,477,-349
-720,670,653
100,12,-28
541,-759,-520
-448,443,-736
-758,682,618
-333,-691,762
33,-147,98
489,-626,862
677,377,661
-508,-650,789
546,-744,772
675,257,784
-757,803,548
-731,-651,-555
-262,475,-694
681,-645,811
681,-763,-383
672,444,677
771,503,-333
-426,499,-674

--- scanner 13 ---
-473,-566,-801
520,720,876
-523,489,329
-725,-624,671
-687,593,327
830,-456,-396
-423,-636,-859
705,-676,642
901,-477,-480
-499,-555,-812
-474,559,369
-703,668,-703
380,824,858
693,712,-347
157,-9,-102
-771,495,-753
911,731,-380
422,710,797
-764,-781,582
759,-804,563
-791,-790,654
-692,513,-593
815,-407,-381
866,624,-337
874,-732,626
-4,-74,33

--- scanner 14 ---
-534,368,659
-788,-761,-491
-878,-818,-609
-464,512,615
25,-4,112
367,-626,-472
-723,-647,724
-581,-714,725
589,352,-779
387,739,618
567,-608,857
-609,480,608
678,-663,846
-638,-792,793
583,-482,843
420,790,648
-158,-135,147
-768,363,-550
669,415,-691
-113,-39,-22
-798,364,-440
-827,-626,-590
504,-766,-476
300,748,687
-937,338,-526
637,476,-829
459,-630,-428

--- scanner 15 ---
-601,-717,591
471,744,-416
633,-619,-476
-856,501,412
-6,160,45
-762,-665,560
628,-610,-709
-753,490,376
-712,540,350
-146,23,18
-580,-372,-608
406,-672,715
-609,-503,-563
-919,430,-399
620,-737,707
-578,-558,-498
511,-717,723
471,610,828
-677,-749,505
-749,437,-318
445,842,-377
443,406,874
655,-563,-506
-909,398,-380
497,717,-299
384,443,802

--- scanner 16 ---
-801,655,-363
482,-522,-398
602,-354,555
-594,-657,-859
-511,864,386
54,93,4
705,-446,598
-789,690,-401
-866,-353,702
600,817,-607
655,-498,603
-104,15,104
-810,-532,745
728,419,691
532,-518,-390
-922,585,-364
531,826,-613
572,799,-788
545,-432,-400
-584,818,474
644,442,520
-618,-637,-695
-817,-412,850
-576,969,447
-562,-699,-673
745,407,639

--- scanner 17 ---
733,-307,-527
-583,868,-693
667,797,-426
70,174,-81
-450,-610,-736
610,-626,389
816,881,-477
-517,909,-589
-346,864,714
593,910,-512
805,-332,-521
816,705,594
819,479,574
-716,-406,591
-641,-619,-639
-568,-679,-796
704,-660,367
740,-636,400
-398,830,724
-554,879,-786
-745,-392,488
-508,808,760
787,639,555
630,-381,-457
-699,-394,397
-23,29,4

--- scanner 18 ---
-609,-497,-861
-749,-667,561
-719,-467,-860
-524,334,-797
-563,485,-870
562,720,-844
-436,349,-851
-508,414,513
535,777,-677
399,582,254
368,-800,319
-698,-880,543
476,536,349
531,593,340
-626,-805,527
591,-765,290
-74,79,4
794,-457,-652
-623,374,462
517,-851,403
-453,367,557
880,-508,-536
-633,-328,-886
746,-466,-501
602,729,-757
75,-21,-160

--- scanner 19 ---
-817,-615,-560
-807,587,607
-639,-366,727
549,-811,-382
-663,635,554
22,94,40
610,495,-385
849,-559,685
-717,866,-533
-855,-755,-581
470,-784,-333
-715,630,504
686,-483,658
-939,821,-534
436,-724,-339
-729,-368,664
360,479,606
491,471,758
-118,21,-56
-643,-358,677
744,531,-309
-814,950,-496
-822,-554,-596
723,423,-403
797,-516,689
367,603,722

--- scanner 20 ---
-707,534,-290
-488,377,466
-491,574,510
859,-343,-465
615,-893,839
-664,-864,-409
-454,-687,819
-560,-651,673
447,597,-654
526,580,-481
920,-448,-400
-762,583,-427
480,415,695
597,557,-563
466,474,537
-786,524,-307
34,-31,22
-527,495,571
-557,-520,806
-687,-714,-397
786,-870,755
-715,-766,-268
832,-494,-473
667,-905,856
482,423,421

--- scanner 21 ---
438,-512,-856
562,-627,-808
516,577,-789
-549,-597,868
43,5,13
646,636,558
-535,-644,-343
-681,-729,884
184,-95,123
474,-636,449
713,494,559
-477,-560,-368
-677,576,-484
-614,567,542
-730,555,-555
602,-535,412
502,-709,-825
-421,-745,871
-648,500,506
-540,-518,-418
553,-745,407
567,594,-761
568,579,490
-773,550,457
-738,684,-506
559,500,-844

--- scanner 22 ---
139,7,36
597,513,782
846,-774,604
729,-705,-721
-554,-568,688
-265,406,-834
-274,-414,-697
-521,706,469
-576,717,580
488,292,-471
658,549,787
-702,703,564
-418,-616,643
-409,-404,-797
-7,-172,97
105,-155,-83
401,335,-584
696,-736,-832
428,286,-505
653,-732,-884
844,-711,694
854,-724,608
457,534,820
-558,-612,780
-388,520,-774
-413,-528,-688
-473,413,-807

--- scanner 23 ---
-470,462,-472
397,-623,373
874,565,-809
-577,597,-441
825,428,359
807,-769,-685
-558,402,-386
-859,601,431
-401,-666,-586
702,-701,-788
840,443,310
-813,611,537
-333,-687,-584
-68,22,-17
-428,-543,-525
84,-16,-148
460,-723,277
-606,-747,663
-823,667,613
-610,-678,801
671,-705,-757
841,347,-828
-581,-728,769
741,357,380
417,-677,355
884,392,-721

--- scanner 24 ---
-583,-471,-740
695,458,-641
-464,566,-502
363,-388,460
437,-318,563
682,528,583
-601,-258,-689
587,-674,-750
732,500,-773
616,-775,-863
-854,-373,600
388,-382,388
-378,582,-453
-741,-287,526
-394,482,-547
-617,-333,-617
-825,-338,377
47,144,1
-583,694,406
-423,713,374
771,572,-697
-649,733,377
694,-725,-882
-119,51,-90
692,539,453
716,506,489

--- scanner 25 ---
825,-589,-478
-5,-111,11
-756,-672,559
-481,-363,-407
-41,69,169
752,-804,598
-686,849,-763
791,651,-479
-463,-418,-541
-622,-677,458
642,-794,644
654,647,-619
-786,-662,525
841,-615,-389
-459,735,657
-496,796,553
489,432,562
-786,859,-628
-444,859,710
-677,735,-624
893,-580,-320
673,-754,645
569,344,499
699,695,-483
-463,-351,-577
521,294,623

--- scanner 26 ---
-795,-443,-559
-804,-231,881
447,-796,-426
787,552,772
323,-746,-474
-939,-241,781
-692,879,-385
-800,-355,772
534,-231,539
761,681,-643
-740,586,891
-655,-439,-696
-948,579,918
719,720,-763
481,-355,554
-109,74,78
725,489,700
386,-658,-394
377,-294,591
710,651,-676
-637,890,-496
-659,-395,-604
-511,876,-480
-825,693,858
736,637,788

--- scanner 27 ---
649,-535,687
-453,717,-503
-656,483,676
-709,-760,-314
-416,655,-698
30,-136,63
550,-661,-354
-96,6,-70
-798,-651,348
-773,-675,517
615,-658,825
-624,490,821
645,-831,-347
-768,534,751
631,-586,900
-782,-721,-465
-416,728,-492
601,-776,-394
-772,-565,487
625,541,-560
758,567,725
-763,-848,-329
810,450,844
796,555,858
543,479,-582
565,396,-625

--- scanner 28 ---
-730,770,-606
-308,-494,617
716,721,-503
938,683,335
-735,560,-557
550,-782,-727
-802,-262,-939
-477,-478,561
494,-587,528
-693,669,-478
852,710,-414
135,108,-167
887,729,301
-662,-244,-835
-423,-557,689
480,-670,572
879,698,-483
-760,-323,-825
-271,560,417
596,-657,-713
781,693,411
469,-779,-724
526,-568,559
-258,576,677
33,-21,-16
-274,708,545

--- scanner 29 ---
-730,-682,812
582,-864,-690
436,772,-425
644,-964,-631
-719,588,-416
486,-851,510
-871,-700,686
-766,-590,650
-803,643,-501
-487,-549,-382
526,815,855
-68,-180,-10
705,-954,-635
-515,388,673
615,801,894
-428,397,713
668,766,771
73,-15,108
477,-750,560
447,736,-322
-490,-581,-412
-595,340,740
461,-900,431
-771,557,-577
-429,-484,-442
486,798,-466

--- scanner 30 ---
-676,-823,-346
-635,-849,-396
-685,-639,809
520,-325,-262
-411,813,-492
483,-484,816
-524,-608,861
-470,913,-554
-719,498,577
548,-371,-466
-466,903,-615
697,-492,881
-600,-694,893
407,740,-578
-779,-787,-357
569,774,-569
88,28,-12
588,-476,861
520,692,592
461,-397,-299
-659,433,519
-689,530,580
572,819,705
611,788,-568
472,733,727
-64,-55,137

--- scanner 31 ---
-723,642,402
-674,-706,617
600,459,590
737,-568,299
585,426,791
-550,618,-599
-561,-322,-516
688,-618,424
-603,474,-551
-633,-679,792
361,-481,-822
-477,-299,-687
327,-643,-921
537,564,-830
538,342,-785
-500,566,-594
-62,44,-15
652,-551,458
-460,-287,-635
-660,552,404
500,467,-914
-605,-697,611
-673,638,394
271,-657,-843
564,464,651

--- scanner 32 ---
677,-408,-808
-395,369,546
627,-359,-933
622,653,-890
583,859,407
601,582,-837
-623,-342,718
610,-444,-774
-607,-328,592
-648,771,-940
-718,-339,537
541,-460,486
-490,657,-922
677,942,399
-401,501,531
-405,-532,-581
-400,-496,-642
683,-461,553
600,887,356
-367,-511,-441
598,-392,408
493,604,-898
-432,436,684
-18,89,12
-580,632,-900
"); //The stored answers are: {1: '405', 2: '12306'}

		var txt = test ? testinput[testindex] : new StreamReader(@"C:\Users\11349\source\repos\Advent\input19.txt").ReadToEnd();// part1 359 part2 12292
		var scanners =  txt.Split("\r\n\r\n").ToList();
		var dictscanners = new Dictionary<int, List<string>>();
		foreach (var scanner in scanners)
		{
			var listscanner = scanner.Trim().Split('\n').ToList();
			listscanner[0] = listscanner[0].Replace($"--- scanner ", "").Replace($" ---", "");
			listscanner = listscanner.Select(o => o.Trim()).ToList();
			dictscanners[int.Parse(listscanner[0])] = listscanner.Skip(1).ToList();
		}
		return dictscanners;
	}

	double distance(string s,string t)
    {
		s = "[" + s + "]";
		t = "[" + t + "]";
		var left = System.Text.Json.JsonSerializer.Deserialize<int[]>(s);
		var right = System.Text.Json.JsonSerializer.Deserialize<int[]>(t);
		if (left.Count() == 3)
		return Math.Sqrt(Math.Pow(left[0]-right[0],2)+ Math.Pow(left[1] - right[1], 2)+ Math.Pow(left[2] - right[2], 2));
		else
			return Math.Sqrt(Math.Pow(left[0] - right[0], 2) + Math.Pow(left[1] - right[1], 2));
	}
	double manhattandistance(string s, string t)
	{
		s = "[" + s + "]";
		t = "[" + t + "]";
		var left = System.Text.Json.JsonSerializer.Deserialize<int[]>(s);
		var right = System.Text.Json.JsonSerializer.Deserialize<int[]>(t);
		return ((left[0] - right[0]) + (left[1] - right[1]) + (left[2] - right[2]));
	}
	string rotate(string s,int ori = 0)
    {
		var t = "[" + s + "]";
		var i = System.Text.Json.JsonSerializer.Deserialize<int[]>(t);
        switch (ori)
        {
			case 1:
				s = i[0]+","+-1*i[2] + ","+i[1];
				break;
			case 2:
				s = i[0] + "," + -1*i[1] + "," + -1*i[2];
				break;
			case 3:
				s = i[0] + "," + i[2] + "," + -1*i[1];
				break;
			case 4:
				s = -1*i[1] + "," + i[0] + "," + i[2];
				break;
			case 5:
				s = ""+i[2] + "," + i[0] + "," + i[1];
				break;
			case 6:
				s = i[1] + "," + i[0] + "," + -1* i[2];
				break;
			case 7:
				s = -1*i[2] + "," + i[0] + "," + -1* i[1];
				break;
			case 8:
				s = -1*i[0] + "," + -1*i[1] + "," + i[2];
				break;
			case 9:
				s = -1* i[0] + "," + -1* i[2] + "," + -1* i[1];
				break;
			case 10:
				s = -1* i[0] + "," + i[1] + "," + -1* i[2];
				break;
			case 11:
				s = -1* i[0] + "," + i[2] + "," + i[1];
				break;
			case 12:
				s = i[1] + "," + -1* i[0] + "," + i[2];
				break;
			case 13:
				s = i[2] + "," + -1* i[0] + "," + -1* i[1];
				break;
			case 14:
				s = -1* i[1] + "," + -1* i[0] + "," + -1* i[2];
				break;
			case 15:
				s = -1* i[2] + "," + -1* i[0] + "," + i[1];
				break;
			case 16:
				s = -1* i[2] + "," + i[1] + "," + i[0];
				break;
			case 17:
				s = i[1] + "," + i[2] + "," +  i[0];
				break;
			case 18:
				s = i[2] + "," + -1* i[1] + "," + i[0];
				break;
			case 19:
				s = -1* i[1] + "," + -1* i[2] + "," + i[0];
				break;
			case 20:
				s = -1* i[2] + "," + -1* i[1] + "," + -1* i[0];
				break;
			case 21:
				s = -1* i[1] + "," + i[2] + "," + -1* i[0];
				break;
			case 22:
				s = i[2] + "," + i[1] + "," + -1* i[0];
				break;
			case 23:
				s = i[1] + "," + -1* i[2] + "," + -1* i[0];
				break;
			default:
                break;
        }
        return s;
	}
	string translate(string s, string t)
	{
		s = "[" + s + "]";
		t = "[" + t + "]";
		var left = System.Text.Json.JsonSerializer.Deserialize<int[]>(s);
		var right = System.Text.Json.JsonSerializer.Deserialize<int[]>(t);
		if (left.Count() == 3)
			return (left[0] - right[0])+","+(left[1] - right[1])+","+(left[2] - right[2]);
		else
			return (left[0] - right[0])+","+ (left[1] - right[1]);
	}
	string negative(string s)
	{
		var t = "[" + s + "]";
		var i = System.Text.Json.JsonSerializer.Deserialize<int[]>(t);
		return -1 * i[0] + "," + -1 * i[1] + "," + -1 * i[2];
	}
	int inverserotation(int i)
    {
		var str = "1,2,3";
		for (int j = 0; j < 24; j++)
		{
			if (str == rotate(rotate(str, i), j))
			{
				return j;
			}
		}
		return -1;
	}
	
	void compute()
	{
		var sw = new System.Diagnostics.Stopwatch();
		sw.Start();
		bool test = true;
		int testindex = 2;
		var scanners = setup(test, testindex);
		var num = scanners.Count;
		var correctoutput = @"-892,524,684
-876,649,763
-838,591,734
-789,900,-551
-739,-1745,668
-706,-3180,-659
-697,-3072,-689
-689,845,-530
-687,-1600,576
-661,-816,-575
-654,-3158,-753
-635,-1737,486
-631,-672,1502
-624,-1620,1868
-620,-3212,371
-618,-824,-621
-612,-1695,1788
-601,-1648,-643
-584,868,-557
-537,-823,-458
-532,-1715,1894
-518,-1681,-600
-499,-1607,-770
-485,-357,347
-470,-3283,303
-456,-621,1527
-447,-329,318
-430,-3130,366
-413,-627,1469
-345,-311,381
-36,-1284,1171
-27,-1108,-65
7,-33,-71
12,-2351,-103
26,-1119,1091
346,-2985,342
366,-3059,397
377,-2827,367
390,-675,-793
396,-1931,-563
404,-588,-901
408,-1815,803
423,-701,434
432,-2009,850
443,580,662
455,729,728
456,-540,1869
459,-707,401
465,-695,1988
474,580,667
496,-1584,1900
497,-1838,-617
527,-524,1933
528,-643,409
534,-1912,768
544,-627,-890
553,345,-567
564,392,-477
568,-2007,-577
605,-1665,1952
612,-1593,1893
630,319,-379
686,-3108,-505
776,-3184,-501
846,-3110,-434
1135,-1161,1235
1243,-1093,1063
1660,-552,429
1693,-557,386
1735,-437,1738
1749,-1800,1813
1772,-405,1572
1776,-675,371
1779,-442,1789
1780,-1548,337
1786,-1538,337
1847,-1591,415
1889,-1729,1762
1994,-1805,1792".Split('\n').Select(o => o.Replace("\n", "").Trim()).ToList();

		var coords = new Dictionary<int, List<(string, string, double)>>();
		var singleperspective = new List<List<string>>();
		foreach (var scanner in scanners)
		{
			coords[scanner.Key] = scanner.Value.SelectMany((a, aindex) =>
			   scanner.Value.Skip(aindex).Where((b, bindex) => a != b)
					 .Select(b => (a, b, 0.0))).ToList();
			singleperspective.Add(new List<string>(scanner.Value));
		}

		foreach (var (key, value) in coords)
		{
			for (int i = 0; i < value.Count; i++)
			{
				value[i] = (value[i].Item1, value[i].Item2, distance(value[i].Item1, value[i].Item2));
			}
		}

		var related = new List<List<(List<string>, int, int, double)>>();
		var overlapping = new List<string>();
		var overlapping1 = new List<string>();
		var pairoverlap = new Dictionary<(int, int), List<string>>();
		var matchedpairbeacon = new Dictionary<(int, int), List<(string, string)>>();
		var beaconpair = new List<Dictionary<string, List<(string, int)>>>();
		var scannerpair = new List<List<int>>();
		var euclid = new Dictionary<(int, int), (int, string)>();
		for (int a = 0; a < num; a++)
		{
			scannerpair.Add(new List<int>());
			beaconpair.Add(new Dictionary<string, List<(string, int)>>());
			related.Add(new List<(List<string>, int, int, double)>());
		}

		for (int i = 0; i < num; i++)
		{
			var matchfound = false;
			for (int j = i + 1; j < num; j++)
			{
				if (j == i)
					continue;
				foreach (var ii in coords[i])
				{
					foreach (var jj in coords[j])
					{
						if (ii.Item3 != jj.Item3)
							continue;
						else
						{
							related[i].Add((new List<string>() { ii.Item1, ii.Item2, jj.Item1, jj.Item2 }, i, j, ii.Item3));
							related[j].Add((new List<string>() { jj.Item1, jj.Item2, ii.Item1, ii.Item2 }, j, i, jj.Item3));
						}
					}
				}

				var left = coords[i].Select(o => o.Item3).ToHashSet();
				var right = coords[j].Select(o => o.Item3).ToHashSet();
				var overlaps = left.Intersect(right);
				overlapping = coords[i].Where(o => overlaps.Contains(o.Item3)).ToList().Select(o => o.Item1).ToList();
				overlapping = overlapping.Concat(coords[i].Where(o => overlaps.Contains(o.Item3)).ToList().Select(o => o.Item2).ToList()).ToList();
				overlapping1 = coords[j].Where(o => overlaps.Contains(o.Item3)).ToList().Select(o => o.Item1).ToList();
				overlapping1 = overlapping1.Concat(coords[j].Where(o => overlaps.Contains(o.Item3)).ToList().Select(o => o.Item2).ToList()).ToList();
				overlapping = overlapping.Distinct().ToList();
				overlapping1 = overlapping1.Distinct().ToList();
				var overlap = overlaps.Count();
				if (overlap >= 66)
				{
					Console.WriteLine($"match between scanner {i} and scanner {j} have overlap {overlap}");
					scannerpair[i].Add(j);
					scannerpair[j].Add(i);
					pairoverlap[(i, j)] = new List<string>(overlapping);
					pairoverlap[(j, i)] = new List<string>(overlapping1);
					//overlapping.ForEach(o=>Console.Write($"({o}), "));
					foreach (var co in overlapping)
					{
						string possiblematch = "";
						string possiblematch1 = "";
						foreach (var match in related[i])
						{
							if ((co != match.Item1[0] && co != match.Item1[1]) || match.Item3 != j)
								continue;
							if (possiblematch == match.Item1[2] || possiblematch == match.Item1[3])
							{
								possiblematch1 = "";
								if (!beaconpair[match.Item2].Keys.Contains(co))
									beaconpair[match.Item2][co] = new List<(string, int)>();
								beaconpair[match.Item2][co].Add((possiblematch, match.Item3));

								if (!beaconpair[match.Item3].Keys.Contains(possiblematch))
									beaconpair[match.Item3][possiblematch] = new List<(string, int)>();
								beaconpair[match.Item3][possiblematch].Add((co, match.Item2));
								break;
							}
							if (possiblematch1 == match.Item1[2] || possiblematch1 == match.Item1[3])
							{
								possiblematch = "";
								if (!beaconpair[match.Item2].Keys.Contains(co))
									beaconpair[match.Item2][co] = new List<(string, int)>();
								beaconpair[match.Item2][co].Add((possiblematch1, match.Item3));

								if (!beaconpair[match.Item3].Keys.Contains(possiblematch1))
									beaconpair[match.Item3][possiblematch1] = new List<(string, int)>();
								beaconpair[match.Item3][possiblematch1].Add((co, match.Item2));
								break;
							}
							possiblematch = match.Item1[2];
							possiblematch1 = match.Item1[3];
						}
					}
					matchfound = true;
					continue;
				}
			}
			if (matchfound)
				continue;
		}

		for (int i = 0; i < beaconpair.Count; i++)
		{
			Console.WriteLine($"scanner {i} has {beaconpair[i].Count} matches");
			foreach (var (key, value) in beaconpair[i])
			{
				for (int j = 0; j < value.Count; j++)
				{
					Console.WriteLine($"{key} from scanner {i} is matched with {value[j].Item1} from scanner {value[j].Item2}");
					if (!matchedpairbeacon.Keys.Contains((i, value[j].Item2)))
						matchedpairbeacon[(i, value[j].Item2)] = new List<(string, string)>();
					matchedpairbeacon[(i, value[j].Item2)].Add((key, value[j].Item1));

					if (!matchedpairbeacon.Keys.Contains((value[j].Item2,i)))
						matchedpairbeacon[(value[j].Item2,i)] = new List<(string, string)>();
					matchedpairbeacon[(value[j].Item2,i)].Add((value[j].Item1,key));
				}
			}
		}

		foreach (var (key, value) in matchedpairbeacon)
		{
			Console.WriteLine($"scanner {key.Item1} and scanner {key.Item2}");
			value.ForEach(o => Console.WriteLine($"{o.Item1}, {o.Item2} "));
			Console.WriteLine();
		}
		for (int i = 0; i < scannerpair.Count; i++)
		{
			Console.WriteLine($"scanner {i} has 12 overlapping beacons with:");
			scannerpair[i].ForEach(o => Console.Write($"{o}, "));
			Console.WriteLine();
		}

        for (int i = 0; i < num; i++)
        {
            foreach (var item in scannerpair[i])
            {
					var one = matchedpairbeacon[(i, item)][4];
					var two = matchedpairbeacon[(i, item)][5];
				for (int x = 0; x < 24; x++)
				{
					var translation = translate(one.Item1, rotate(one.Item2, x));
					var translation1 = translate(two.Item1, rotate(two.Item2, x));
					var both = translation == translation1;
					if (both)
					{
						euclid[(i, item)] = (x, translation);
						break;
					}
				}
			}
        }
		foreach (var (key, value) in euclid)
		{
			Console.WriteLine($"to get to scanner {key.Item1} from scanner {key.Item2} rotate by {value.Item1} and then translate by {value.Item2}");
		}

		List<string> change(int i, int j, List<string> list)
		{
			if (list is null)
				return null;
			if (i == j)
				return list;
			if (euclid.Keys.Contains((i, j)))
				return list.Select(o => translate(rotate(o, euclid[(i, j)].Item1), negative(euclid[(i, j)].Item2))).ToList();
			if (euclid.Keys.Contains((j, i)))
				return list.Select(o => rotate(translate(o,euclid[(j, i)].Item2), inverserotation(euclid[(j, i)].Item1))).ToList();
				//return list.Select(o => translate(rotate(o, inverserotation(euclid[(j, i)].Item1)), euclid[(j, i)].Item2)).ToList();
            return null;
		}
		var known = new List<int>();
		List<string> oldrecursivechange(int i, int j, List<string> list, bool recursivecall=false)
		{
			if (!recursivecall)
				known = new List<int>();
			known.Add(i);
			known.Add(j);
			if (i == j)
				return list;
			if (euclid.Keys.Contains((i, j)) || euclid.Keys.Contains((j, i)))
			{
				return change(i, j, list);
			}
			var options = new List<List<string>>();
			foreach (var x in scannerpair[i].Where(o=>scannerpair[j].Contains(o)).ToList())
            {
				if (!known.Contains(x))
				{
					known.Add(x);
					var option = change(i, x, recursivechange(x, j, list, true));
					if (option != list && option!=change(i,x,option))
						return option;
				}
			}
            foreach (var x in scannerpair[i])
            {
				if (!known.Contains(x))
				{
					known.Add(x);
					var option = change(i, x, recursivechange(x, j, list, true));
					if (option != list && option != change(i, x, option))
						return option;
				}
			}
			return list;
		}

		List<string> recursivechange(int i, int j, List<string> list, bool recursivecall = false)
		{
			if (list is null)
				return null;
			if (!recursivecall)
				known = new List<int>();
			known.Add(i);
			known.Add(j);
			if (i == j)
				return list;
			if (euclid.Keys.Contains((i, j)) || euclid.Keys.Contains((j, i)))
				return change(i, j, list);
			var options = new List<List<string>>();
			foreach (var x in scannerpair[i].Where(o => scannerpair[j].Contains(o)).ToList())
			{
				if (!known.Contains(x))
				{
					known.Add(x);
					var trypath = recursivechange(x, j, list, true);
					if (trypath is null)
						continue;
					var option = change(i, x,trypath);
					if (option is not null)
						return option;
				}
			}
			foreach (var x in scannerpair[i])
			{
				if (!known.Contains(x))
				{
					known.Add(x);
					var trypath = recursivechange(x, j, list, true);
					if (trypath is null)
						continue;
					var option = change(i, x,trypath);
					if (option is not null)
						return option;
				}
			}
			return null;
		}


		for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num; j++)
            {
				if (i == j)
					continue;
				var shuffled = recursivechange(i, j, scanners[j]);
				if (shuffled is null)
					continue;
				singleperspective[i] = singleperspective[i].Union(shuffled).ToList();
            }
        }
		int b = 0;
        foreach (var item in singleperspective)
        {
            Console.WriteLine($"Scanner {b++} sees {item.Count} total beacons. {item.Distinct().Count()} of them are unique");
        }
		
		Func<int,int,string> distancetox = (a,b) =>  recursivechange(a, b, new List<string>() { "0,0,0" })[0];
		
		var manhattan = new List<List<double>>();
        for (int d = 0; d < num; d++)
        {
			manhattan.Add(new List<double>());
        }
		for (int i = 0; i < num; i++)
		{
            for (int j = i+1; j < num; j++)
            {
                for (int x = 0; x < num; x++)
                {
                    var pairdistance = manhattandistance(distancetox(x, i), distancetox(x, j));
                    manhattan[x].Add(pairdistance > 0 ? pairdistance : -1 * pairdistance); 
                }
            }
		}
            Console.WriteLine($"part 1: {singleperspective[0].Count()} part 2: {manhattan.SelectMany(o=>o).Max()}");
		sw.Stop();
		Console.WriteLine($"Elapsed; {sw.ElapsedMilliseconds}ms");
    }
    compute();
}

static void day20()
{
    string setup(bool test = true,int testindex = 0)
    {
        var testinput = new List<string>();
		testinput.Add(@"..#.#..#####.#.#.#.###.##.....###.##.#..###.####..#####..#....#..#..##..###..######.###...####..#..#####..##..#.#####...##.#.#..#.##..#.#......#.###.######.###.####...#.##.##..#..#..#####.....#.#....###..#.##......#.....#..#..#..##..#...##.######.####.####.#.#...#.......#..#.#.#...####.##.#......#..#...##.#.##..#...##.#.##..###.#......#.#.......#.#.#.####.###.##...#.....####.#..#..#.##.#....##..#.####....##...##..#...#......#.#.......#.......##..####..#...#.#.#...##..#.#..###..#####........#..####......#..#

#..#.
#....
##..#
..#..
..###");
		testinput.Add(@"##.......#.####..#..#...##.##...#.####.#.##.######...###...#..#####...###...##.###..#...#....#.#....###.#....##..####...##....###....##.#.###..###.#...####.#....#.#.#.####...#...#.#..#....#######......#..#.###.....#.#...#.....##.##.##..#....##.##.####.#..#..#.#...#..########.##.########....##.#...#...#..#..#.#..#..###..#..##.##.#.#.#.##.##..##....#.##.#.#.##.#..#..####..#...####.##..#.....###...##..##..####..#..#...####.##.##.#.##.#....####..####...#.#.#..#.#.##.##.##.###.#......##.#...#.#.##..##.##..###...

##.##.#.#####..###.######.#.######..##...#.#.##....#..#...#...##.##.#######...###...#..#.##..#.##.##
.###..####..#..#....##..#.#.#.####.##.#.#####.####.#.....#.##.#####.#.....#...##.#.###.......#.#####
..###.#..##.....#.###.#.##..##...##.##..##.###.##.#..#..#....###.##..#...#.#.##...###..##.......####
.##...#.###...##..#.##..##....#.#.#####.#.#.###..##...#.##.#.....#..#.........#.#.##.#...##.#....###
##..##..#..#.####.#...##....#.......#...#.#.#..##..#.###..#.#.####.#..##..###...####..###...#.....#.
...#..#....##.#..#.#..#...###..#...#.##.##...#...####.#.#.#.###.##.###.####..##.##....#..#.###....##
..#.####.....###......###......##.##.###...##.....#.#..##...###..##..###....##..#.###.####...##.###.
#...##..##.#.#..#.##.##.#.##.....#...##.#...#....#......######.#...###....#....####.#.###.###...#.#.
#..##..###...#.#..##..###..#....##.#.#####.#.##.##.#....###.####.##..#...####..###..###.#...#.#.###.
#.##.#######.###.###..###.......##.##...#...####.##...##..##.###.#..#####.#..###..#......#..#####..#
.#####....#####..#..###.##....##.#....#.#..#...##...#.#.#..####.#.#..##.#....####...#...##..##.#.###
#.##.#..#.#.###.##..#.##.#......###..##.###.#...############.##...##.##..#.#.#.#..#.##..##..#.#..##.
##.#.##.#..##.#....#..#..#.#.##.####....#......###.##.#.##.#.##.#.#..#.#.#...#...##....##.####...#..
#..##.##.##......##...##..#.######.#.#.##...####.#.....#...#.#..#....#...#.#####.....##.#....##.#.#.
#.#...##.#.#..#.##.##.....###..#.##.#...##.#.......#.######..###########.###.#.#.##...#...#.#.#..###
.##.#..#...#...#.##..###...##########.##.####....##.#.##########.#..#.#.##########....##......#.#.##
#..##.###.#..####....#.###..#.#.###.##.#######.##.#####.#......###.#..#####....##.#...#.#######.....
#.....####........#......#.#.#.##..#.#..#.#.#.#.##..#######...#...###.##..#.#..#...#.#.....##.#.#...
#.#.#.####..##...###.#####.########......#.#.##.##...##.#...#.#.#...#..#..#.....##.....##.#..#.#...#
...#.##.#..###..###.#..##...##.##..#####....###...###.#.#########.#..#...#....##..#.##.#.###.####...
##.#...#..#..#.#..##..#.#######.#.#..###....#....####..##.##..#.##.#.####.##..#####.##.#...#.##..#.#
......#.#.#....#.###....########....##....####..###.##.####.......#.#..###...####..###.####.##..#.#.
#...##.#.###.#.###...#.#..#..#....#....####.###.#.####.#####.#.#...#.#.#.####..#.####.....##.#..#.##
....###..#.##########......##.####..##.####....##.#.####.##..##......##.##..##.####..#####..#.#.##.#
#.#.###.###........#..#.#.##.#..#.###.#.#.##..#.#.#.##.#########..#..#....##..#....#.##...#.##.#..#.
#...#..###.######..#.###.#####....###..#..#...#...###.#.#####.#..##..####..##...#.#.##..###.#....###
###.#..#.##.#.#...#.#.#..#..#...#....#.......###.......#.#.##...#.#.....#...#.....#.#....#####...#.#
..#.......#.#.#.##.....##.####.####...#..##.#.##....#.##.#.#.####..#.#.###.....###..####...##.#.#...
###.####.#####.....#...#.###.#...#.#...###.#....#..#.#######...#####..#....###.....#######..#####.#.
.#####.#.#.#.##..#####.####.#.#.#.#.#...##..##......##..#.###.#..#...#...####...#.#.##....#.####..#.
...#.#.##..#...######.##.#.###..####.....#..##..#.####..#.#.##.##..#.###.###..##...##.#.#.#.#.##.##.
...#...##.#.####.#.#..#..#..#.#.#.#.###.####.#....###...#.....##.###..#..##..##.##.......##.##.##.##
###.##...##.#####.#.#..#.#..#.#..######.#....#..##.######...#..##.#####....#.##..###.##..#.#...#....
##.....##.#..#####..#...#.#.#####.....#..###.##..#..#..#.#.....#.#..##.#####..##...#.###...##.#.###.
#..#..##..#..#.###.#..#..#.##..#.#####.##.....#.##......#.##.######.##.###.##...#..####...#...##.###
.#.###...####...#....#..##.#.#..##.#.##.###..#..#.##.#########...#.#..#..##...####.#.#.#.##......###
#...#...#.###.##..#..#...#####.#######......##.#.....#.#......#..####..#..##..#.#..#######...#.#..#.
.#####..#..#...#.###...###....#...#..##.###...#.#.#...###.##....#...##...#.#..#..##.###.#.###....##.
#.#.#.#.......##..#.#.#.###..###...##..##..#.####....##.####...#...##..#.#..#.####.######..######...
.#......#.##..#.####.#..#.#.#######.#..###.#..##...#...#..##.###.#..######...#..###########..###.###
...##...#...##.#.##.###..##.####.#.#..#.##.##..##.#.#.....#.##.#...#....#.##.#....#....##.#.#.####.#
.#.#....#.#..#....#..##....#......##.##...###.....#...#.#...#..#..##..##...##.#..#.####..####...#.#.
..##.#...###....#.#.######.#.#....######.......#..#...###.###.##.##.#..#.###.##..#.#.#..###########.
.##.#..#.#.#....#..###.######.#....#########....#...#..##..##...#.#####.###.##.####...#..#..#.#.##..
####.#..#....##...#...#.#.##.#.#.##.#.#...#...##.....#..##..###.#.#.#.#####......#..#....##..##.##..
#.#..##.#..##.##.###.###...#....#####.##....#.#..#.#..#####..#.#..###.#..#..#......##.#.##..#.#.#..#
###....#.........#.##.#.##.#.#.###.####.###..#.####.#####.####.....#....#..#...##.##..##.####...#.##
##..###.##.########.#.##.##...#.#..###.##.###.####.###..#..#...#.#.#..##..###.#.#.##...#####.#.#..##
.#####..###..#.#..#....##..###.#.....#...###....#....##..###.##..####.#..#.#......###..#....#...#..#
.......#.##....##..#...##....#..##...#.##.#..##..#..#######..##..#.#....#....#..####.#.#..#.#.#.##..
....###.#..#..#....#..#..#.##.....#..##..###.#.#.###.##.#..#....####.##.......##.#..#..#....###....#
...####.##.#.########.#.##.##...####.#.###.##..###..##.....##.#...#.........#....##.....#.####.#.##.
#..##....#.##....##..#...#.#.##.####....#......##..##.##.#.#.#.#....#.####..##.#.#..########..#...##
.####.....#..##...#.##..##...#.#..#.#...#####.#...#..##....###.###.#.##.####..#.#.#..#...#..#.#.#...
#.###..#...#.#.##.######....#..#...##..##.###.....##..#.#.#..#.##.#.####..######........##..#.##....
.###..##....##..#.###.##..#..##.#.#.....#..##.#####.#.#.#....###...###......#.#..#####...##....###..
.#.........#..##...##.##.##..#...###.#####.#####.#..#....##....##..##...##.#..##.###..##..##........
.#.##...#..#..#.#####...#..###.###.#.#....######.##....#.####.####...#######.....#.#.#.#.#..#..#...#
..###.##..#....#######.#..##.#.##.###.##.#.#..##..###.#.###.....#..##.##..##.#..##.....#.##.#.#####.
###.....#...###.#....##..##....#..#..###...###..#....###.##.#####.#...#....#.####.###......##..####.
####....###..##.#.#.#..########..####..##.#...........#######.###.#..#.###.##.##.....##.....###.#.#.
#.###.###..#.#.###.....##..###......#....#.#...##.###...###..#..####..#.....#...##..##......#...#.##
.....#.........#...###.####.#...###.###..#.#.##..#.###..##..##..#......#.#######...##.###.#.#.##...#
##.######...#...##......#..#.######.###.#####.....#.#.#...####.#.#...#.###..#.##....#######...#...##
.#..#.........########.##..####.#########..##.....###.##.#.#..#######..#..######..##.#.###.######..#
#.#.#.#.##.#.....##.#.....###...#..##.#.#.###....#.#...##..##.###....###....#.####..###....##.####..
..#.#...##.#######..#.#...#.##...##.#.###.###.#.#.##.#.##.##..#.##..#.#....#.#.#.####......###...##.
#.#....##.#....#.#.###..##.#.#.#######...#.###.#.#.#....##...##.#.#..#...########.##.###.#.#.#....#.
#.....#....######...###.#.##...#.#..##..#.#.#....###..#......#..#........##.##.#..####.#.#.##.#.####
#.#..#..#..#.##.#..#.###...##.##....#....###.##.#####..#..##.#.#.#..#.###..##..#####......###.###.##
#.#......#.#.###.#..#...#......#.....#..#..#..#.#.....##.#.##..##.###.#.#..#.#.#....#....#.#....##..
######.#.##.##.#.#...#.##..#..########.#.####....##.######....##.##.....#.###....####...#.#.##..##.#
..##.###..##.#.##..#..#.######.#.#.#.####.#....##..##..#.#.#....##...###.##..####...#....#######..#.
##.##.#.....###..#####.#.###..#.#.#...##..#.######.#######..#.##.##.##..##...#####...##.##.#.###..##
#.#..##.##....#...##.#..#.#####..##..#..##.#.##.#.##...##.##...##.###........#.############.....#...
...#.####..##.#.#..#.#.#..##...####.#..#.#....##..##..#####.#..#...##.##..###..###..##...##...##...#
#.###..#.....#.#.#######.#..#..##.###..#..#.##.#..####.#...#.####.##.#....##...##..##..##..#..###..#
###..##.###.#...##..##....###..#.#..#.#.##.....#..##.#..##..#.##......#..##..#.#..##..#.####...##.#.
...##...#.#.##.#...###.##....#.#.#.##..#####.#.##.#.##.###..#..#.##.#..###.####.#...#....#.#.#.#.#.#
...#.#...####...#####.....#.#.##.#.#..##.###..#.#..##.###.#.#....#.##..####..#....#.##.###....####..
...##....##..#.#..#####..#...###..##..#.#..#..###.....##..#..###.#.###....#..#.##.#..###.#....##..##
.#..####.#.#..###.##.#.###.#.###.######.####.#.##....#.#...#.#####..#.##..##......#..#.#.##.##.####.
....##.#.#.....###..##.#..#..#####.####.#.#.##.####..##.##.#.##..#.###.....####.###..#.##..#...#.#..
#.##.#..#.###.#.###..#.##.##.##.#...#...#.#....#..#.##.#.....#....###.#.###..######......#.###.##.##
#..##.#.#........##.#.###.#.#.#.#..##.##.###.#.###.#....#.##.#####.##..###..###..##.#.....#.#####...
#####.##..#.#.##..#...#...#.#.####.#.#.##.#....#.#..##...#.#.#.#.#..#....##.....#.#..#####..##.##...
.#.##...##.####...#..###.##.#.#....##...#.##.##.#.#..#.#..##..##.###.#.#..#.#..##.#...#..#....#.#..#
######......###.#..#....##.#######...##.####.#....#..####.#..##.#..#..#.######.#.#.######..#.#..##.#
###...#.####.#...#.##.####...#....##.#..#.#..###..#..#....#.###.#.#.#.#...##....#..#..##.#.###....##
.#.##......##..#...##....###...#..###.#.#......##.###.#.###.#..#####..#...###.####.######.###.###..#
###.#.#..##...#..#.#.#....###...##..##.###.#.###.##..####.#.##.####.##..##...#......#######.#..##.##
....##..#.#...##......##.#.#.#.####.#.#......#.#..........#....##...#.##.#.#.###..##.###.####..####.
#......####.#.#####.##.###.##...######..#.#..###.##......###..#..#.#...#.####.#.##...###..#.###..###
.#....###.##.####.##..####..##.##..######.#..#.###.#...##..#####...#..###.#.#.#######.##.####.###.#.
###.#.#.#.###.#.#.###....##.....###.#..##.####..#.##..##.##########.#.#..#..###..##..#.####......###
...##..##..#####.#..#.###..#...####....#..#.####..#...###.##.###.###...#.#..#...#..#.#..###..#....#.
.###.#.#####.#....#..######.####.###..##..###...##...####..#.....##.##.#.....#..###.####.....#.##.##
.#....##.#..##.....###....##.#.#####...#..#..###..#......#.#....#...#.#.#....###...#..####..#.#...#.
#....#..##..##..#.###..#..#.#.....#.#.##.#####..#.#.##...##..##..#.##.##.#..#...#.##.#...#.#.#....##
#.#...#.###..##.##.#########......#.##..###.###...##...#..##...#.#...#..###.#######.#####..#.##.#.#.
"); //{1: '5339', 2: '18395'}
		var txt = test ? testinput[testindex] : new StreamReader(@"C:\Users\11349\source\repos\Advent\input20.txt").ReadToEnd(); // part 1: 5571 part 2: 17965
        return txt;
    }
	int enhancecounter = 0;
	char valueorinfinte(int[][]image,int x,int y)
    {
        if (x < 0 || y < 0)
            return enhancecounter % 2 != 0 ? '0' : '1';
        if (x > image.Length - 1 || y > image.Length - 1)
            return enhancecounter % 2 != 0 ? '0' : '1';
		//if (x < 0 || y < 0)
		//          return '0';
		//if (x > image.Length-1 || y > image.Length-1)
		//	return '0';
		return image[x][y] == 1 ? '1' : '0';

    }
	int ninepixels(int[][] image, (int ,int) coord,string map)
    {
		var (x, y) = coord;
		char[] binary = "222222222".ToArray();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
				binary[(3 * (j+1)) + (i+1)] = valueorinfinte(image, x + j, y + i);
			}
        }
		var bin = new string(binary);
		var mapindex = Convert.ToInt32(new string(bin), 2);
		return map[mapindex] == '.' ? 0 : 1;
    }
	int[][] enhance(int[][] image, string map)
    {
		if(map[0]=='#' && map[map.Length-1]=='.')
            enhancecounter++;
		int len = image.Length;
		var newimage = new int[len + 2][];
        for (int i = 0; i < len+2; i++)
        {
			newimage[i] = new int[len + 2];
        }

        for (int i = 0; i < len+2; i++)
        {
            for (int j = 0; j < len+2; j++)
            {
				newimage[i][j] = ninepixels(image, (i-1, j-1), map); 
            }
        }
		return newimage;
    }

    void compute()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
		var input = setup(false, 1);
		var image = input.Split("\r\n\r\n")[1];
		var map = input.Split("\r\n\r\n")[0];
		int len = (int)Math.Sqrt(image.Length);
		var imagearray = new int[len][];
        for (int i = 0; i < len; i++)
        {
            imagearray[i] = new int[len]; 
        }
        for (int i = 0; i < len; i++)
        {
            for (int j = 0; j < len; j++)
            {
				imagearray[j][i] = image.Split('\n')[j].Replace("\n", "").Trim()[i] == '.' ?0:1;
			}
		}
		//print2d(imagearray);
        //Console.WriteLine(image);

        var once = enhance(imagearray, map);
        var twice = enhance(once, map);
        //print2d(once);
        Console.WriteLine();
        //print2d(twice);
        var lit = 0;
        for (int i = 0; i < twice.Length; i++)
        {
            for (int j = 0; j < twice.Length; j++)
            {
                lit += twice[i][j];
            }
        }
        Console.WriteLine($"after twice {lit} pixels are lit!");
		//var zeroarray = new int[5][];
		//      for (int i = 0; i < 5; i++)
		//      {
		//	zeroarray[i] = new int[] { 0, 0, 0, 0, 0 };
		//      }
		//var tryy = enhance(zeroarray,map);
		//print2d(tryy);
		//Console.WriteLine($"{ninepixels(imagearray, (0, 0), map)}");
		var output = imagearray;
        for (int i = 0; i < 50; i++)
        {
			output = enhance(output, map);
        }
		lit = 0;
		for (int i = 0; i < output.Length; i++)
		{
			for (int j = 0; j < output.Length; j++)
			{
				lit += output[i][j];
			}
		}
		Console.WriteLine($"after 50 {lit} pixels are lit!");


        sw.Stop();
        Console.WriteLine($"Elapsed; {sw.ElapsedMilliseconds}ms");
    }
    compute();
}

static void day21a()
{
    List<int> setup(bool test = true, int testindex = 0)
    {
        var testinput = new List<string>();
		testinput.Add(@"Player 1 starting position: 4
Player 2 starting position: 8");
        var txt = test ? testinput[testindex] : new StreamReader(@"C:\Users\11349\source\repos\Advent\input21.txt").ReadToEnd();
        return txt.Split('\n').Select(o=>int.Parse(o.Trim().Split(": ")[1])).ToList();
    }

	var totaldice = 0;
	List<int> rolldice(int dice)
    {
		var returnval = new List<int>();
        for (int i = 0; i < 3; i++)
        {
			totaldice++;
			dice++;
			if (dice == 101)
				dice = 1;            
			returnval.Add(dice);
        }
		return returnval;
    }
    void compute()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
		var start = setup(false, 0);
		var player = new List<int>[2];
		player[0] = new List<int>(){start[0],0 };
		player[1] = new List<int>(){start[1],0 };
		//var player2 = new { Pos = start[1], Score = 0 };
		int who = 0;
		bool over = false;
		int dice = 0;
		while(!over)
        {
			var roll = rolldice(dice);
			dice = roll[2];
			var tomove = roll.Sum();
                while (tomove > 0)
                {
				player[who][0]++;
				if (player[who][0] == 11)
					player[who][0] = 1;
				tomove--;
                }
			player[who][1] += player[who][0];
			if (player[who][1] >= 1000)
				break;
			who = (who + 1) % 2;

        }
        Console.WriteLine($"part 1: should be: {totaldice}*{player[who==0?1:0][1]} = {totaldice*player[who == 0 ? 1 : 0][1]}");

        sw.Stop();
        Console.WriteLine($"Elapsed; {sw.ElapsedMilliseconds}ms");
    }
    compute();
}

static void day21b()
{
	List<int> setup(bool test = true, int testindex = 0)
	{
		var testinput = new List<string>();
		testinput.Add(@"Player 1 starting position: 4
Player 2 starting position: 8");
		var txt = test ? testinput[testindex] : new StreamReader(@"C:\Users\11349\source\repos\Advent\input21.txt").ReadToEnd();
		return txt.Split('\n').Select(o => int.Parse(o.Trim().Split(": ")[1])).ToList();
	}

	var rolltotal = new Dictionary<int, ulong>() { { 3, (ulong)1 }, { 4, (ulong)3 }, { 5, (ulong)6 }, { 6, (ulong)7 }, { 7, (ulong)6 }, { 8, (ulong)3 }, { 9, (ulong)1 } };
		int add(int x,int y)
        {
			return x + y > 10 ? x + y - 10 : x + y;
        }
	void compute()
	{
		var sw = new System.Diagnostics.Stopwatch();
		sw.Start();
		ulong player1wins = (ulong)0;
		ulong player2wins = (ulong)0;
        var scorecount = new Dictionary<(int, int,int,int,bool), ulong> { { (0,0,4,7, true),(ulong)1} };
        
		(ulong,ulong) recursive(int score1,int score2,int pos1,int pos2,bool turn)
        {
			if (score1 >= 21 )
				return (1,0);
			if (score2>=21 )
				return (0,1);
			ulong item1 = 0;
			ulong item2 = 0;
			
			foreach (var (roll, count) in rolltotal)
			{
				if (turn)
				{
					int newpos = add(pos1, roll);
					int newscore = score1 + newpos;
					var recursivecall = recursive(newscore, score2, newpos, pos2, !turn);
					item1 += (recursivecall.Item1 * count);
					item2 += (recursivecall.Item2 * count);
				}
				else
				{
					int newpos = add(pos2, roll);
					int newscore = score2 + newpos;
					var recursivecall = recursive(score1, newscore, pos1, newpos, !turn);
					item1 += (recursivecall.Item1 * count);
					item2 += (recursivecall.Item2 * count);
					
				}
			}
			return (item1,item2);
		}

		for (int j = 0; j < 20; j++)
        {
            var oldkeys = scorecount.Keys.Where(x=>(x.Item1<21)&&(x.Item2<21)).ToList();
            foreach (var key in oldkeys)
            {
                var value = scorecount[key];
                foreach (var (roll, count) in rolltotal)
                {
					if (key.Item5)
					{
						int newpos = add(key.Item3, roll);
						int newscore = key.Item1 + newpos;
						if (!scorecount.Keys.Contains((newscore, key.Item2, newpos, key.Item4, !key.Item5)))
						{
						scorecount[(newscore, key.Item2, newpos, key.Item4, !key.Item5)] = value * count;
					}
					else
							scorecount[(newscore, key.Item2, newpos, key.Item4, !key.Item5)] += value * count;
                        if (newscore >= 21)
                        {
                            player1wins += value * count;
                        }
                    }
					else
					{
						int newpos = add(key.Item4, roll);
						int newscore = key.Item2 + newpos;
						if (!scorecount.Keys.Contains((key.Item1, newscore, key.Item3, newpos, !key.Item5)))
						{
                        scorecount[(key.Item1, newscore, key.Item3, newpos, !key.Item5)] = value * count;
					}
					else
						scorecount[(key.Item1, newscore, key.Item3, newpos, !key.Item5)] += value * count;
                        if (newscore>=21)
                        {
                            player2wins += value * count;
                        }
                    }
                }
            }
            //Console.WriteLine($"after iteration {j} there are:");
			//Console.WriteLine(scorecount.Keys.Where(n => n.Item1 >= 21 || n.Item2 >= 21).Count());
            oldkeys.ForEach(a => scorecount.Remove(a));
        }
      
        Console.WriteLine($"total unique brances after turns = {scorecount.Values.Count}");
		Console.WriteLine($"player 1 won: {player1wins} and player 2 won : {player2wins}");

		
			sw.Stop();
		Console.WriteLine($"Elapsed; {sw.ElapsedMilliseconds}ms");
	}
	compute();
}


static void day22()
{
    List<string> setup(bool test = true,int index=0)
    {
        var testinput = new List<string>();
		testinput.Add(@"on x=10..12,y=10..12,z=10..12
on x=11..13,y=11..13,z=11..13
off x=9..11,y=9..11,z=9..11
on x=10..10,y=10..10,z=10..10");
		testinput.Add(@"on x=-20..26,y=-36..17,z=-47..7
on x=-20..33,y=-21..23,z=-26..28
on x=-22..28,y=-29..23,z=-38..16
on x=-46..7,y=-6..46,z=-50..-1
on x=-49..1,y=-3..46,z=-24..28
on x=2..47,y=-22..22,z=-23..27
on x=-27..23,y=-28..26,z=-21..29
on x=-39..5,y=-6..47,z=-3..44
on x=-30..21,y=-8..43,z=-13..34
on x=-22..26,y=-27..20,z=-29..19
off x=-48..-32,y=26..41,z=-47..-37
on x=-12..35,y=6..50,z=-50..-2
off x=-48..-32,y=-32..-16,z=-15..-5
on x=-18..26,y=-33..15,z=-7..46
off x=-40..-22,y=-38..-28,z=23..41
on x=-16..35,y=-41..10,z=-47..6
off x=-32..-23,y=11..30,z=-14..3
on x=-49..-5,y=-3..45,z=-29..18
off x=18..30,y=-20..-8,z=-3..13
on x=-41..9,y=-7..43,z=-33..15
on x=-54112..-39298,y=-85059..-49293,z=-27449..7877
on x=967..23432,y=45373..81175,z=27513..53682");
		testinput.Add(@"on x=-5..47,y=-31..22,z=-19..33
on x=-44..5,y=-27..21,z=-14..35
on x=-49..-1,y=-11..42,z=-10..38
on x=-20..34,y=-40..6,z=-44..1
off x=26..39,y=40..50,z=-2..11
on x=-41..5,y=-41..6,z=-36..8
off x=-43..-33,y=-45..-28,z=7..25
on x=-33..15,y=-32..19,z=-34..11
off x=35..47,y=-46..-34,z=-11..5
on x=-14..36,y=-6..44,z=-16..29
on x=-57795..-6158,y=29564..72030,z=20435..90618
on x=36731..105352,y=-21140..28532,z=16094..90401
on x=30999..107136,y=-53464..15513,z=8553..71215
on x=13528..83982,y=-99403..-27377,z=-24141..23996
on x=-72682..-12347,y=18159..111354,z=7391..80950
on x=-1060..80757,y=-65301..-20884,z=-103788..-16709
on x=-83015..-9461,y=-72160..-8347,z=-81239..-26856
on x=-52752..22273,y=-49450..9096,z=54442..119054
on x=-29982..40483,y=-108474..-28371,z=-24328..38471
on x=-4958..62750,y=40422..118853,z=-7672..65583
on x=55694..108686,y=-43367..46958,z=-26781..48729
on x=-98497..-18186,y=-63569..3412,z=1232..88485
on x=-726..56291,y=-62629..13224,z=18033..85226
on x=-110886..-34664,y=-81338..-8658,z=8914..63723
on x=-55829..24974,y=-16897..54165,z=-121762..-28058
on x=-65152..-11147,y=22489..91432,z=-58782..1780
on x=-120100..-32970,y=-46592..27473,z=-11695..61039
on x=-18631..37533,y=-124565..-50804,z=-35667..28308
on x=-57817..18248,y=49321..117703,z=5745..55881
on x=14781..98692,y=-1341..70827,z=15753..70151
on x=-34419..55919,y=-19626..40991,z=39015..114138
on x=-60785..11593,y=-56135..2999,z=-95368..-26915
on x=-32178..58085,y=17647..101866,z=-91405..-8878
on x=-53655..12091,y=50097..105568,z=-75335..-4862
on x=-111166..-40997,y=-71714..2688,z=5609..50954
on x=-16602..70118,y=-98693..-44401,z=5197..76897
on x=16383..101554,y=4615..83635,z=-44907..18747
off x=-95822..-15171,y=-19987..48940,z=10804..104439
on x=-89813..-14614,y=16069..88491,z=-3297..45228
on x=41075..99376,y=-20427..49978,z=-52012..13762
on x=-21330..50085,y=-17944..62733,z=-112280..-30197
on x=-16478..35915,y=36008..118594,z=-7885..47086
off x=-98156..-27851,y=-49952..43171,z=-99005..-8456
off x=2032..69770,y=-71013..4824,z=7471..94418
on x=43670..120875,y=-42068..12382,z=-24787..38892
off x=37514..111226,y=-45862..25743,z=-16714..54663
off x=25699..97951,y=-30668..59918,z=-15349..69697
off x=-44271..17935,y=-9516..60759,z=49131..112598
on x=-61695..-5813,y=40978..94975,z=8655..80240
off x=-101086..-9439,y=-7088..67543,z=33935..83858
off x=18020..114017,y=-48931..32606,z=21474..89843
off x=-77139..10506,y=-89994..-18797,z=-80..59318
off x=8476..79288,y=-75520..11602,z=-96624..-24783
on x=-47488..-1262,y=24338..100707,z=16292..72967
off x=-84341..13987,y=2429..92914,z=-90671..-1318
off x=-37810..49457,y=-71013..-7894,z=-105357..-13188
off x=-27365..46395,y=31009..98017,z=15428..76570
off x=-70369..-16548,y=22648..78696,z=-1892..86821
on x=-53470..21291,y=-120233..-33476,z=-44150..38147
off x=-93533..-4276,y=-16170..68771,z=-104985..-24507
");
        var txt = test ? testinput[index] : new StreamReader(@"C:\Users\11349\source\repos\Advent\input22.txt").ReadToEnd();
        return txt.Trim().Split('\n').ToList();
    }
    void compute()
    {
        var sw = new System.Diagnostics.Stopwatch();
        sw.Start();
		var cubes = new Dictionary<(int, int, int), bool>();
		var totalon = 0;
		var input = setup(true, 2);

		ulong intersect(((int,int),(int,int),(int,int)) x, ((int, int), (int, int), (int, int)) y)
        {
			var ((x11, x12), (y11, y12), (z11, z12)) = x;
			var ((x21, x22), (y21, y22), (z21, z22)) = y;
			var x1range = Enumerable.Range(x11, x12 - x11+1).ToList();
			var y1range = Enumerable.Range(y11, y12 - y11+1).ToList();
			var z1range = Enumerable.Range(z11, z12 - z11+1).ToList();
			var x2range = Enumerable.Range(x21, x22 - x21+1).ToList();
			var y2range = Enumerable.Range(y21, y22 - y21+1).ToList();
			var z2range = Enumerable.Range(z21, z22 - z21+1).ToList();
			var deltax = x1range.Intersect(x2range);
			var deltay = y1range.Intersect(y2range);
			var deltaz = z1range.Intersect(z2range);
			return(ulong)deltax.Count()*(ulong)deltay.Count()*(ulong)deltaz.Count();
        }

		//part1
        foreach (var line in input)
        {
			var on = "on" == line.Split(" ")[0].Trim();
			var coords = line.Split(" ")[1].Trim();
			var x = coords.Split(",")[0].Split("=")[1].Split("..");
			var y = coords.Split(",")[1].Split("=")[1].Split("..");
			var z = coords.Split(",")[2].Split("=")[1].Split("..");
			int x1 = int.Parse(x[0]);
			int x2 = int.Parse(x[1]);
			int y1 = int.Parse(y[0]);
			int y2 = int.Parse(y[1]);
			int z1 = int.Parse(z[0]);
			int z2 = int.Parse(z[1]);
            x1 = int.Parse(x[0])<-50?-50:int.Parse(x[0]);
			x2 = int.Parse(x[1]) >50?50:int.Parse(x[1]);
			y1 = int.Parse(y[0])<-50?-50:int.Parse(y[0]);
			y2 = int.Parse(y[1]) >50?50:int.Parse(y[1]);          
			z1 = int.Parse(z[0])<-50?-50:int.Parse(z[0]);
			z2 = int.Parse(z[1]) >50?50:int.Parse(z[1]);
			for (int i = x1; i < x2 + 1; i++) 
            {
				for (int j = y1; j < y2 + 1; j++)
                {
					for (int k = z1; k < z2 + 1; k++)
                    {
						if (i >= -50 && i <= 50 && j >= -50 && j <= 50 && k >= -50 && k <= 50) 
                        {
							if (!cubes.Keys.Contains((i, j, k)))
								cubes[(i, j, k)] = false;
							if (cubes[(i, j, k)] && !on)
								totalon--;
							if (!cubes[(i, j, k)] && on)
								totalon++;
                        }
						cubes[(i, j, k)] = on;
                    }
                }
            }
        }
		Console.WriteLine($"{totalon}");

		//part2
		var intersections = new List<((int, int), (int, int), (int, int),bool)>();
		ulong part2 = 0;
		var pon = "on" == input[0].Split(" ")[0].Trim();
		var pcoords = input[0].Split(" ")[1].Trim();
		var px = pcoords.Split(",")[0].Split("=")[1].Split("..");
		var py = pcoords.Split(",")[1].Split("=")[1].Split("..");
		var pz = pcoords.Split(",")[2].Split("=")[1].Split("..");
		int px1 = int.Parse(px[0]);
		int px2 = int.Parse(px[1]);
		int py1 = int.Parse(py[0]);
		int py2 = int.Parse(py[1]);
		int pz1 = int.Parse(pz[0]);
		int pz2 = int.Parse(pz[1]);
		var fcube = ((px1, px2), (py1, py2), (pz1, pz2), pon);
		intersections.Add(fcube);
		if(!fcube.Item4)
		part2 -= (ulong)intersect((fcube.Item1, fcube.Item2, fcube.Item3), (fcube.Item1, fcube.Item2, fcube.Item3));
		else
			part2 += (ulong)intersect((fcube.Item1, fcube.Item2, fcube.Item3), (fcube.Item1, fcube.Item2, fcube.Item3));
		var cubeoverlap = new Dictionary<((int, int), (int, int), (int, int), bool), List<((int, int), (int, int), (int, int), bool)>>();
		for(int i =1; i<input.Count;i++)
        {
			var line = input[i];
			var on = "on" == line.Split(" ")[0].Trim();
			var coords = line.Split(" ")[1].Trim();
			var x = coords.Split(",")[0].Split("=")[1].Split("..");
			var y = coords.Split(",")[1].Split("=")[1].Split("..");
			var z = coords.Split(",")[2].Split("=")[1].Split("..");
			int x1 = int.Parse(x[0]);
			int x2 = int.Parse(x[1]);
			int y1 = int.Parse(y[0]);
			int y2 = int.Parse(y[1]);
			int z1 = int.Parse(z[0]);
			int z2 = int.Parse(z[1]);
			var cube = ((x1, x2), (y1, y2), (z1, z2),on);
			cubeoverlap[cube] = new List<((int, int), (int, int), (int, int), bool)>();
			if(cube.Item4)
			part2 += intersect((cube.Item1, cube.Item2, cube.Item3), (cube.Item1, cube.Item2, cube.Item3));
			var noofoverlaps = 0;
			foreach (var pcube in intersections)
            {
				var overlap = intersect((cube.Item1, cube.Item2, cube.Item3), (pcube.Item1, pcube.Item2, pcube.Item3));
                if (overlap>0)
                {
					cubeoverlap[cube].Add(pcube);
					noofoverlaps++;
					if (cube.Item4 == pcube.Item4 == true)
						part2 -= overlap;
                }
			}
			intersections.Add(cube);
		}
        Console.WriteLine(part2);
		var a = ((-4, 4), (-4, 4), (-4, 4));
		var b = ((-4, 4), (-4, 4), (-4, 4));
        Console.WriteLine(2758514936282235);
		//Console.WriteLine($"{intersect(a,b)}");

        sw.Stop();
        Console.WriteLine($"Elapsed; {sw.ElapsedMilliseconds}ms");
    }
    compute();
}

//static void dayx()
//{
//	void setup(bool test = true)
//	{
//		var testinput = @"test";

//		var txts = File.ReadAllLines(@"C:\Users\11349\source\repos\Advent\input11.txt");
//		var txt = test ? testinput : new StreamReader(@"C:\Users\11349\source\repos\Advent\inputx.txt").ReadToEnd();
//		return txt.Select();
//	}
//	void compute()
//	{
//	bool test = true;
//	var sw = new System.Diagnostics.Stopwatch();
//	sw.Start();

//	sw.Stop();
//	Console.WriteLine($"Elapsed; {sw.ElapsedMilliseconds}ms");
//	}
//	compute();
//}

static void Main()
{
	//day1();
	//day2();
	//day3();
	//day4();
	//day5();
	//day6();
	//day7();
	//day8();
	//day9();
	//day10();
	//day11();
	//day12();
	//day13();
	//day14();
	//day15();
	//day16();
	//day17();
	//day18();
	//day19();
	//day20();
	//day21a();
	//day21b();
	day22();
}


Main();