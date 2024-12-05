f = open('data.txt', 'r')

matrix = []
for l in f.readlines():
    l = l.strip()

    matrix.append([])
    for c in l:
        matrix[-1].append(c)

f.close()

sum = 0
for i in range(1, len(matrix)-1):
    l = matrix[i]

    for j in range(1, len(l)-1):
        c = l[j]
        if c == "A":
            nw = matrix[i-1][j-1]
            ne = matrix[i-1][j+1]

            sw = matrix[i+1][j-1]
            se = matrix[i+1][j+1]

            r = nw+se
            x = ne+sw

            if "S" not in r or "M" not in r:
                continue

            if "S" not in x or "M" not in x:
                continue

            sum += 1

print(sum)
