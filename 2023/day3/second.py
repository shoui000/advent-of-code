f = open('data.txt', 'r')

matrix = []
symbols = []
numbers = []
for i, l in enumerate(f.readlines()):
    l = l.strip()

    for j, c in enumerate(l):
        matrix.append(c)

        if c == '*':
            symbols.append([i,j])

        if c.isdigit():



f.close()

