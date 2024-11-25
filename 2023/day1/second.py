import re
f = open('data.txt', 'r')

numbers = {
        'one' : 'one1one',
        'two' : 'two2two',
        'three' : 'three3three',
        'four' : 'four4four',
        'five' : 'five5five',
        'six' : 'six6six',
        'seven' : 'seven7seven',
        'eight' : 'eight8eight',
        'nine' : 'nine9nine',
        'zero' : 'zero0zero'
}

nmbs = []
for l in f.readlines():
    l = l.rstrip()

    for n in numbers:
        l = l.replace(n, numbers[n])

    x = re.findall(r'\d', l)
    num = x[0] + x[-1]

    nmbs.append(num)

f.close()

sum = 0
for n in nmbs:
    sum += int(n)

print(sum)
