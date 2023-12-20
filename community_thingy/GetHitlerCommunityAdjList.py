def filter_adjacency_list(module_file, adjacency_list_file, output_file):
    # Step 1: Read the module file and store node IDs
    module_nodes = set()
    with open(module_file, 'r') as mod_file:
        for line in mod_file:
            parts = line.strip().split()
            if len(parts) > 0:
                module_nodes.add(parts[0].strip())  # Node IDs as strings

    print(f"Number of nodes in module 18: {len(module_nodes)}")

    # Step 2: Process the adjacency list
    with open(adjacency_list_file, 'r') as adj_file, open(output_file, 'w') as out_file:
        filtered_node_count = 0
        node_id = 0  # Initialize node ID

        for line in adj_file:
            node_id_str = str(node_id)  # Convert node ID to string for comparison

            if node_id_str in module_nodes:
                adjacent_nodes = line.strip().split()
                filtered_nodes = [node for node in adjacent_nodes if node in module_nodes]

                if filtered_nodes:
                    out_file.write(f"{node_id_str}: {' '.join(filtered_nodes)}\n")
                    filtered_node_count += 1
                else:
                    # Print node ID that has no adjacent nodes in Module 18
                    print(f"Node {node_id_str} in Module 18 has no links to other nodes in Module 18.")

            node_id += 1  # Increment node ID for next line

    print(f"Number of nodes written to output: {filtered_node_count}")

module_file_path = 'community_thingy//module_18_output.txt'
adjacency_list_file_path = 'AdjList.txt'
output_file_path = 'filtered_adjacency_list.txt'

filter_adjacency_list(module_file_path, adjacency_list_file_path, output_file_path)
