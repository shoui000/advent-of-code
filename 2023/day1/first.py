import re

f = open("data.txt", "r")

numbers = []
for l in f.readlines():
    l = l.rstrip()

    x = re.findall(r'\d', l)
    num = x[0] + x[-1]

    numbers.append(num)

f.close()

sum = 0
for n in numbers:
    sum += int(n)

print(sum)
