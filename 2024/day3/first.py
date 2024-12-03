import re
f = open('data.txt', 'r')

sum = 0
for l in f.readlines():
    l = l.strip()

    x = re.findall(r'mul\((\d{1,3}),(\d{1,3})\)', l)

    for mul in x:
        sum += int(mul[0]) * int(mul[1])

f.close()

print(sum)
