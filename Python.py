import numpy as np
import matplotlib.pyplot as plt

def read_data(file_path):
    with open(file_path, 'r') as file:
        data = np.array([int(line.strip()) for line in file])
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
    plt.hist(data, bins=100000, color='blue', alpha=0.7)
    plt.xscale('log')  # Set the x-axis to logarithmic scale
    plt.title('In-degree Distribution')
    plt.xlabel('Number of Links (log scale)')
    plt.ylabel('Frequency')
    plt.show()

def main():
    file_path = 'InDegrees.txt'  # Replace with your file path
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
