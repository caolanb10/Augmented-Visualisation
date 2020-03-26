import random

CATEGORIES = ['Star', 'Cube', 'Sphere']

NUMBER_OF_LINES = 100

HEADER = "Height, Weight, Age, Category"

test_file = open('test.csv', 'w')

test_file.write(HEADER + '\n')

for i in range(NUMBER_OF_LINES):
    test_file.write(
        str(random.randint(0, 200)) + ', ' +
        str(random.randint(0, 100)) + ', ' +
        str(random.randint(0, 90)) + ', ' +
        CATEGORIES[random.randint(0, len(CATEGORIES) - 1)
        ])
    test_file.write('\n')
