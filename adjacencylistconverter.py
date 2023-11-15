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

def main():
    file_path = 'C:\CsharpCsharp\Degrees_of_Hitler\wiki-topcats.txt'  # Replace with the path to your file
    edges = read_edge_list(file_path)
    adjacency_list = convert_to_adjacency_list(edges)
    print(adjacency_list)

if __name__ == "__main__":
    main()
