def calculate_clustering_coefficient(adjacency_list_file):
    with open(adjacency_list_file, 'r') as file:
        # Process each line to create a dictionary with node id as key and neighbors as values
        adjacency_dict = {}
        for line in file:
            parts = line.strip().split(':')
            if len(parts) == 2:
                node = int(parts[0])
                neighbors = parts[1].split()
                adjacency_dict[node] = neighbors

    # Function to calculate the clustering coefficient of a single node
    def clustering_coefficient(node, neighbors):
        if len(neighbors) < 2:
            return 0  # No edges possible between less than 2 nodes

        actual_edges = 0
        for neighbor in neighbors:
            # Check for connections between the neighbors
            neighbor_neighbors = adjacency_dict.get(int(neighbor), [])
            actual_edges += len([n for n in neighbor_neighbors if n in neighbors])

        possible_edges = len(neighbors) * (len(neighbors) - 1)
        return actual_edges / possible_edges

    # Calculate clustering coefficient for each node
    coefficients = []
    for node, neighbors in adjacency_dict.items():
        coeff = clustering_coefficient(node, neighbors)
        coefficients.append(coeff)

    # Average clustering coefficient
    average_coefficient = sum(coefficients) / len(coefficients)
    return average_coefficient

# Replace this with the path to your adjacency list file
adjacency_list_file_path = 'filtered_adjacency_list.txt'

average_clustering_coefficient = calculate_clustering_coefficient(adjacency_list_file_path)
print(f"The average clustering coefficient of the network is: {average_clustering_coefficient}")
