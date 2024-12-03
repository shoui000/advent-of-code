import re
f = open('data.txt', 'r')

sum = 0
active = True
for l in f.readlines():
    l = l.strip()

    x = re.findall(r"(mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don't\(\))", l)

    for mul in x:
        if mul[0] == "don't()":
            active = False

        if mul[0] == "do()":
            active = True

        if active == True and mul[1] != '':
            sum += int(mul[1]) * int(mul[2])

f.close()

print(sum)
