import random
from datetime import date

today = date.today()
yesterday = date(today.year, today.month, today.day - 1)

print(today)
print(yesterday)

NUMBER_OF_LINES = 100
CATEGORIES = ['Star', 'Cube', 'Sphere']
HEADER = "Date, Height, Weight, Age, Category"

test_file = open('test.csv', 'w')

test_file.write(HEADER + '\n')

for i in range(NUMBER_OF_LINES):
    date = yesterday if i < NUMBER_OF_LINES/2 else today
    test_file.write(
        str(date) + ', ' +
        str(random.randint(0, 200)) + ', ' +
        str(random.randint(0, 100)) + ', ' +
        str(random.randint(0, 90)) + ', ' +
        CATEGORIES[random.randint(0, len(CATEGORIES) - 1)
        ])
    test_file.write('\n')
