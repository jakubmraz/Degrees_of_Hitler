def read_edge_list(file_path):
    with open(file_path, 'r') as file:
        edges = [line.strip().split() for line in file.readlines()]
    return edges

def convert_to_adjacency_list(edges):
    adjacency_list = {}
    for edge in edges:
        if edge[0] not in adjacency_list:
            adjacency_list[edge[0]] = []
        if edge[1] not in adjacency_list:
            adjacency_list[edge[1]] = []
        adjacency_list[edge[0]].append(edge[1])
        adjacency_list[edge[1]].append(edge[0])  # Remove this line if the graph is directed
    return adjacency_list

def save_adjacency_list(adj_list, output_file_path):
    with open(output_file_path, 'w') as file:
        for key, values in adj_list.items():
            file.write(f"{key}: {' '.join(values)}\n")

# In the main function
def main():
    file_path = 'C:/CsharpCsharp/Degrees_of_Hitler/wiki-topcats.txt'  # Replace with the path to your file
    output_file_path = 'C:/CsharpCsharp/Degrees_of_Hitler/adjacency list.txt'  # Replace with the desired output file path
    edges = read_edge_list(file_path)
    adjacency_list = convert_to_adjacency_list(edges)
    save_adjacency_list(adjacency_list, output_file_path)

if __name__ == "__main__":
    main()