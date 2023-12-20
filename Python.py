import numpy as np
import matplotlib.pyplot as plt

def read_data(file_path):
    with open(file_path, 'r') as file:
        data = [int(line.strip()) for line in file]
    return data

def five_number_summary(data):
    minimum = np.min(data)
    q1 = np.percentile(data, 25)
    median = np.median(data)
    q3 = np.percentile(data, 75)
    maximum = np.max(data)
    return minimum, q1, median, q3, maximum

def plot_distribution(data):
    plt.figure(figsize=(10, 6))
    counts, bins, patches = plt.hist(data, bins='auto', color='blue', alpha=0.7)

    # Adding count above each bar
    for count, patch in zip(counts, patches):
        plt.text(patch.get_x() + patch.get_width() / 2, count + 0.5, str(int(count)), 
                 ha='center', va='bottom')

    plt.title('Distribution of Shortest Paths to Hitler')
    plt.xlabel('Number of Links')
    plt.ylabel('Frequency')
    plt.show()

def main():
    file_path = 'ShortestPaths.txt'  # Replace with your file path
    data = read_data(file_path)

    # Five-number summary
    min_val, q1, median, q3, max_val = five_number_summary(data)
    print("Five-Number Summary:")
    print("Minimum:", min_val)
    print("Q1:", q1)
    print("Median:", median)
    print("Q3:", q3)
    print("Maximum:", max_val)

    # Plot
    plot_distribution(data)

if __name__ == "__main__":
    main()
