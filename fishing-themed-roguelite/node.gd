extends Node


func _ready() -> void:
    var viewport_size: Vector2 = get_viewport().get_visible_rect().size
    print("Viewport size: ", viewport_size)
